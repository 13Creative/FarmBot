using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

#pragma warning disable 1998

namespace Farm_Bot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        private string name;
        private int age;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;

            await this.SendWelcomeMessageAsync(context);
        }

        private async Task SendWelcomeMessageAsync(IDialogContext context) {
            await context.PostAsync("Hello, Welcome To The FarmBot QNA System!");

            context.Call(new NameDialog(), this.NameDialogResumeAfter);
        }

        private async Task NameDialogResumeAfter(IDialogContext context, IAwaitable<string> result) {
            try
            {
                this.name = await result;

                context.Call(new AgeDialog(this.name), this.AgeDialogResumeAfter);
            }
            catch (TooManyAttemptsException) {
                await context.PostAsync("I couldn't understand you, let us try again!");

                await this.SendWelcomeMessageAsync(context);
            }
        }

        private async Task AgeDialogResumeAfter(IDialogContext context, IAwaitable<string> result) {
            try
            {
                this.age = Int32.Parse(await result);

                await context.PostAsync($"Your Name Is {name} and your age is {age}");
            }
            catch (TooManyAttemptsException)
            {
                await context.PostAsync("I couldn't understand you, let us try again!");
            }
            finally {
                await this.SendWelcomeMessageAsync(context);
            }
        }
    }
}