using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RebornCheckerBoardMain.Models
{
        /// <summary>
        /// The set of types of tokens that may be issued.
        /// </summary>
        public enum TokenType
        {
            /// <summary>
            /// A game or a DLC.
            /// </summary>
            Game,

            /// <summary>
            /// A subscription to a service.
            /// </summary>
            Subscription
        }
}