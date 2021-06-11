namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class ApplyWrapper
    {
        private readonly dynamic _action;

        public ApplyWrapper(dynamic action)
        {
            this._action = action;
        }

        public void apply(dynamic param1)
        {
            this._action(param1);
        }

        public void apply(dynamic param1, dynamic param2)
        {
            this._action(param1, param2);
        }

        public void apply(dynamic param1, dynamic param2, dynamic param3)
        {
            this._action(param1, param2, param3);
        }
    }

    // ReSharper restore InconsistentNaming
}
