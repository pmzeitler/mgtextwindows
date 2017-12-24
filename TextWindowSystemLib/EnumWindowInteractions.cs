
namespace net.PhoebeZeitler.TextWindowSystem
{
    /// <summary>
    /// A convenience class that standardizes all possible user inputs to an active window.
    /// </summary>
    public enum WindowInteractions
    {
        // Confirm/advance
        OK,
        // Cancel one
        CANCEL,
        // Close all windows
        CANCEL_ALL,
        // d-pad/arrow up
        CURSOR_UP,
        // d-pad/arrow down
        CURSOR_DOWN,
        // d-pad/arrow left
        CURSOR_LEFT,
        // d-pad/arrow right
        CURSOR_RIGHT,
        // advance a counter by ten
        CURSOR_BIGUP,
        // decrement a counter by ten
        CURSOR_BIGDOWN,
        // jump to the top of a menu/list
        CURSOR_JUMPTOP,
        // jump to the bottom of a menu/list
        CURSOR_JUMPBOTTOM

    }

    /// <summary>
    /// A convenience class that encapsulates reactions to user input on an active window.
    /// </summary>
    public class WindowResponse
    {
        private bool operationCompleted = false;
        public bool OperationCompleted { get { return operationCompleted; } }
        private bool keepWindowOpen = false;
        public bool KeepWindowOpen { get { return keepWindowOpen; } }
        private bool payloadExists = false;
        public bool PayloadExists { get { return payloadExists; } }

        private object payload = null;
        public object Payload { get { return payload; } }

        public WindowResponse(bool operationCompleted, bool keepWindowOpen, bool payloadExists, object payload)
        {
            this.operationCompleted = operationCompleted;
            this.keepWindowOpen = keepWindowOpen;
            this.payloadExists = payloadExists;
            this.payload = payload;
        }
    }
}
