namespace Farm_Bot.Dialogs {

    using Microsoft.Bot.Builder.Dialogs;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class EmailDialog : IDialog<string> {

        private string email;
        private int attempts = 3;

        public EmailDialog() {
        }

        public async Task StartAsync(IDialogContext context) {
            await context.PostAsync($"Please Tell Me The Email That you would like to register on our mailing list.");

            context.Wait(this.MessageRecievedAsync);
        }

        private async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result) {
            var message = await result;

            string email = message.Text;
            if (email.Contains("@")) {
                context.Done(email);
            }
            else {
                --attempts;
                if (attempts > 0) {
                    await context.PostAsync("I'm sorry, Please Check That you Typed in A Valid Email Address.");

                    context.Wait(this.MessageRecievedAsync);
                }
                else {
                    context.Fail(new TooManyAttemptsException("Message was not a valid email."));
                }
            }
        }
    }
  
}