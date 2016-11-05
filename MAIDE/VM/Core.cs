using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace MAIDE.VM
{
    public class Core
    {
        public enum State
        {
            NoBuild,
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

        public void Invoke(Stream stream)
        {
            if (status != State.Ready && status != State.Finish)
                return;

            var reader = new BinaryReader(stream);
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

            try
            {
                while (status == State.Launched || status == State.Pause)
                {
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

                    // if (op.Row.IsFlag(CodeEditBox.RowFlag.Breakpoint))
                    //     Pause();

                    // op.Row.SetFlag(CodeEditBox.RowFlag.Run);
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
                            //  throw new RuntimeException(string.Format(Language.RuntimeException, op.Method.Name), getCurrentRow());
                        }

                        // if (sr.EndOfStream)
                        //    throw new RuntimeException(Language.RuntimeExceptionMemory, op.Row.Index + 1);

                        if (needPause)
                        {
                            needPause = false;
                            Pause();
                        }

                        Pointer++;
                        total++;
                    }

                    //  op.Row.ResetFlag(CodeEditBox.RowFlag.Run);
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
            if (status == State.Launched || status == State.Pause || status == State.Finish)
            {
                // if (Pointer < source.Count)
                //     source[Pointer].Row.ResetFlag(CodeEditBox.RowFlag.Run);

                Status = State.Ready;
                waitEvent.Set();
            }
        }
    }
}