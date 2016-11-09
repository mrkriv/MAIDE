using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MAIDE.UI;
using Row = MAIDE.UI.CodeEditBox.RowReadonly;
using Rows = MAIDE.UI.CodeEditBox.RowReadonlyCollection;

namespace MAIDE.VM
{
    public class Core
    {
        public enum State
        {
            Ready,
            Launched,
            Pause,
            Finish,
            Error,
        }

        public class StateChangedEventArgs : EventArgs
        {
            public State Old { get; set; }
            public State New { get; set; }

            public StateChangedEventArgs(State In, State To)
            {
                Old = In;
                New = To;
            }
        }

        private ManualResetEvent waitEvent;
        private EventHandler<StateChangedEventArgs> stateChanged;
        private int total;
        private State status;
        private bool needPause;
        private Row currentRow;

        public readonly Dictionary<string, int> Sections;
        public readonly Stack<int> Stack;
        public int Pointer = 0;

        public State Status
        {
            get { return status; }
            private set
            {
                if (status != value)
                {
                    State old = status;
                    status = value;
                    stateChanged.Invoke(this, new StateChangedEventArgs(old, value));
                }
            }
        }

        public event EventHandler<StateChangedEventArgs> StateChanged
        {
            add
            {
                lock (this) { stateChanged += value; }
            }
            remove
            {
                lock (this) { stateChanged -= value; }
            }
        }

        public Core()
        {
            Stack = new Stack<int>();
            Sections = new Dictionary<string, int>();

            stateChanged = (s, e) => { };
            waitEvent = new ManualResetEvent(true);
        }

        public void Invoke(Stream stream, Rows codeRows = null)
        {
            Operators.Core = this;

            Stack.Clear();
            Pointer = 0;
            total = 0;
            needPause = false;
            waitEvent.Set();
            Status = State.Launched;

            int i = 500;
            while (Console.Instance == null)
            {
                Thread.Sleep(5);
                if (--i == 0)
                {
                    MessageBox.Show(Language.ConsoleDontAnswer);
                    return;
                }
            }

            Console.Clear();

            var reader = new BinaryReader(stream);
            stream.Seek(0, SeekOrigin.Begin);

            var debugTable = getDebugTable(reader, codeRows);

            try
            {
                while (status == State.Launched || status == State.Pause)
                {
                    currentRow = debugTable != null ? debugTable[(short)stream.Position] : null;
                    Operation op = OperationManager.Decode(reader);

                    if (total > Properties.Settings.Default.TotalTickLimit)
                    {
                        if (MessageBox.Show(Language.ProgrammLoop, Language.ProgrammLoopTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Status = State.Error;
                            return;
                        }
                        total = -total;
                    }

                    if (currentRow != null && currentRow.IsFlag(CodeEditBox.RowFlag.Breakpoint))
                        Pause();

                    if (currentRow != null)
                        currentRow.SetFlag(CodeEditBox.RowFlag.Run);

                    waitEvent.WaitOne();

                    if (status == State.Launched)
                    {
                        try
                        {
                            op.Method.Invoke(null, op.Args);
                        }
                        catch (TargetInvocationException e)
                        {
                            if (e.InnerException is RuntimeException)
                                throw e.InnerException;
                            throw new RuntimeException(string.Format(Language.RuntimeException, op.Method.Name), getCurrentRowIndex());
                        }

                        if (needPause)
                        {
                            needPause = false;
                            Pause();
                        }

                        Pointer += op.Length;
                        total++;
                    }

                    if (currentRow != null)
                        currentRow.ResetFlag(CodeEditBox.RowFlag.Run);
                }
                Status = State.Finish;
            }
            catch (RuntimeException e)
            {
                Console.WriteLine(string.Format(Language.RuntimeExceptionRow, e.Row, e.Message));
                Status = State.Error;
            }

            Console.Write(Language.PressAnyKey);
            Console.MoveCaretToEnd();
            Console.ReadKey();
        }

        private int getCurrentRowIndex()
        {
            return currentRow != null ? currentRow.Index + 1 : -1;
        }

        private Dictionary<short, Row> getDebugTable(BinaryReader reader, Rows rows)
        {
            if (rows == null)
                return null;

            Dictionary<short, Row> debugTable = null;
            var stream = reader.BaseStream;

            while (stream.Position < stream.Length)
            {
                if (reader.ReadByte() != 0xFF)
                    continue;

                debugTable = new Dictionary<short, Row>();

                while (stream.Position < stream.Length)
                {
                    short pos = reader.ReadInt16();
                    short row = reader.ReadInt16();
                    debugTable.Add(pos, rows[row]);
                }
            }
            stream.Seek(0, SeekOrigin.Begin);

            return debugTable;
        }

        public void Pause()
        {
            waitEvent.Reset();
            Status = State.Pause;
        }

        public void Resume(bool oneOperation = false)
        {
            if (status == State.Pause)
            {
                Status = State.Launched;
                needPause = oneOperation;
                waitEvent.Set();
            }
        }

        public void Stop()
        {
            if (currentRow != null)
                currentRow.ResetFlag(CodeEditBox.RowFlag.Run);

            Status = State.Ready;
            waitEvent.Set();
        }
    }
}