//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using Microsoft.Bot.Builder.Dialogs;
//using Microsoft.Bot.Connector;

//namespace Farm_Bot
//{
//    [BotAuthentication]
//    public class MessagesController : ApiController
//    {
//        /// <summary>
//        /// POST: api/Messages
//        /// Receive a message from a user and reply to it
//        /// </summary>
//        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
//        {
//            if (activity.Type == ActivityTypes.Message)
//            {
//                await Conversation.SendAsync(activity, () => new EchoDialog());
//            }
//            else
//            {
//                HandleSystemMessage(activity);
//            }
//            var response = Request.CreateResponse(HttpStatusCode.OK);
//            return response;
//        }

//        private Activity HandleSystemMessage(Activity message)
//        {
//            if (message.Type == ActivityTypes.DeleteUserData)
//            {
//                // Implement user deletion here
//                // If we handle user deletion, return a real message
//            }
//            else if (message.Type == ActivityTypes.ConversationUpdate)
//            {
//                // Handle conversation state changes, like members being added and removed
//                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
//                // Not available in all channels
//            }
//            else if (message.Type == ActivityTypes.ContactRelationUpdate)
//            {
//                // Handle add/remove from contact lists
//                // Activity.From + Activity.Action represent what happened
//            }
//            else if (message.Type == ActivityTypes.Typing)
//            {
//                // Handle knowing tha the user is typing
//            }
//            else if (message.Type == ActivityTypes.Ping)
//            {
//            }

//            return null;
//        }
//    }

//    [System.Serializable]
//    public class EchoDialog : IDialog<object>
//    {

//        protected int count = 1;

//        public async Task StartAsync(IDialogContext context)
//        {
//            await context.PostAsync("Hello, Welcome To The Farmbot QNA System!"); // Putting here is useless.
//            context.Wait(MessageReceivedAsync);
//        }

//        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
//        {
//            var message = await argument;
//            if (message.Text == "reset")
//            {
//                PromptDialog.Confirm(context, AfterResetAsync, "Are you sure you want to reset the count?", "Didn't Get That!", promptStyle: PromptStyle.None);
//            }
//            else
//            {
//                await context.PostAsync($"{this.count++}:You said {message.Text}");
//                context.Wait(MessageReceivedAsync);
//            }
//        }

//        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
//        {
//            var confirm = await argument;
//            if (confirm)
//            {
//                this.count = 1;
//                await context.PostAsync("Reset Count.");
//            }
//            else
//            {
//                await context.PostAsync("Did not reset Count.");
//            }
//            context.Wait(MessageReceivedAsync);
//        }
//    }
//}