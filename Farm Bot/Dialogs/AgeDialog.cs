namespace Farm_Bot.Dialogs
{
    using Microsoft.Bot.Builder.Dialogs;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class AgeDialog : IDialog<string>
    {
        private string name;
        private int attempts = 3;

        public AgeDialog(string name) {
            this.name = name;
        }

        public async Task StartAsync(IDialogContext context) {
            await context.PostAsync($"{this.name}, what is your age?");

            context.Wait(this.MessageRecievedAsync);
        }

        private async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result) {
            var message = await result;

            int age;
            if (Int32.TryParse(message.Text, out age) && (age > 0)){
                context.Done(Convert.ToString(age));
            }
            else {
                --attempts;
                if (attempts > 0){
                    await context.PostAsync("I'm sorry, I didnt understand your reply. What is your age (e.g. '15')?");

                    context.Wait(this.MessageRecievedAsync);
                }
                else {
                    context.Fail(new TooManyAttemptsException("Message was not a valid age."));
                }
            }
        }
    }
  
}