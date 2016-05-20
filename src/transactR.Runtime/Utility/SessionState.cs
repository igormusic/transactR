using System;
using System.Threading;

namespace TransactRules.Core.Utilities

{
    public class SessionState

    {
        private  const string SESSION_KEY  = "_session_";

        private DateTime? _valueDate;
        private DateTime? _actionDate;

        private static Object lockObject = new Object();
        public static ThreadLocal<SessionState> _localSession;

        public DateTime ValueDate {
            get 
            {
                if (!_valueDate.HasValue)
                {
                    _valueDate = ActionDate;
                }

                return _valueDate.GetValueOrDefault();
            }
 
            set
            {
                
                _valueDate = value;
            }
        }
        public DateTime ActionDate 
        {
            get 
            {
                if (!_actionDate.HasValue)
                {
                    _actionDate = DateTime.Today.Date;
                }

                return _actionDate.GetValueOrDefault();
            }
            set 
            {
                _actionDate = value;
            }
        }



        public static SessionState Current
        {
          get
            {
                SessionState result = null;

                result = GetSessionStateFromThreadContext(result);
                
                return result;
            }
        }

        private static SessionState GetSessionStateFromThreadContext(SessionState result)
        {
            if (_localSession.IsValueCreated==false)
            {
                _localSession.Value = new SessionState();
            }
            return _localSession.Value;
        }
  
    }
}
