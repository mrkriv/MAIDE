using MAIDE.UI;

namespace MAIDE
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public int Row { get; set; }
        public CodeEditBox CodeBlock;

        public ErrorMessage(string message, int index, CodeEditBox block)
        {
            CodeBlock = block;
            Message = message;
            Row = index;
        }
    }
}