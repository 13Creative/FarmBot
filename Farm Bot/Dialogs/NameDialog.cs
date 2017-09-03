namespace Farm_Bot.Dialogs {
    using Microsoft.Bot.Builder.Dialogs;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class NameDialog : IDialog<string>{
        private int attempts = 3;

        public async Task StartAsync(IDialogContext context) {
            await context.PostAsync("What is your name?");

            context.Wait(this.MessageRecievedAsync);
        }

        private async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result) {
            var message = await result;

            // Test if name is valid.
            if ((message.Text != null) && (message.Text.Trim().Length > 0)) {
                context.Done(message.Text);
            }
            else {
                --attempts;
                if (attempts > 0) {
                    await context.PostAsync("I'm Sorry, I Dont understand your reply. What is your name ( e.g. 'Bill', 'Jc', 'Duke' )?");

                    context.Wait(this.MessageRecievedAsync);
                }
                else {
                    context.Fail(new TooManyAttemptsException("Message was not a string or was an empry string"));
                }
            }
        }
    }
}