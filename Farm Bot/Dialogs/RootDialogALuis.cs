using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Farm_Bot.Dialogs;

namespace Farm_Bot {

    [LuisModel("2537a8b7-17a5-4b2d-8d64-c0f5cfc976fa", "c7c14f2df06a42719fd4f9b748fd1963")]
    [Serializable]
    public class RootDialogALuis : LuisDialog<object> {

        [LuisIntent("LearnMore")]
        public async Task GiveInfo(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result) {
            var message = await activity;
            await context.PostAsync("You Can Find Out More On Our Website, Located At : https://www.1point3creative.com");
            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result) {
            await context.PostAsync("Sorry we didnt understand your message, please try again.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("PreOrder")]
        public async Task PreOrderDialog(IDialogContext context, LuisResult result) {
            await context.PostAsync("Pre-Orders Are Available, Would you Like Me To Take You Through The Process?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Download App")]
        public async Task DownloadApp(IDialogContext context, LuisResult result) {
            await context.PostAsync("Sorry. We Currently dont have any apps available.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("ThankYou")]
        public async Task Thanks(IDialogContext context, LuisResult result) {
            await context.PostAsync("My Pleasure! I aim only to please.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result) {
            await context.PostAsync("Hello! What Can I Do For You Today?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("JoinCommunity")]
        public async Task JoinCommunity(IDialogContext context, LuisResult result) {
            await context.PostAsync("Please Have a look at : https://www.1point3creative-demo/jc-science for our community!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("JoinMailingList")]
        public async Task JoinMail(IDialogContext context, LuisResult result) {
            if (result.Entities.Count != 0)
            {
                await context.PostAsync($"Entities Detected in Mailing List Intent : {result.Entities[0].Entity}");
                context.Wait(MessageReceived);
            }
            else {
                // Spawn Sub Dialog Asking For Email Address.
                context.Call<string>(new EmailDialog(),this.AfterEmail);
            }
        }

        private async Task AfterEmail(IDialogContext context, IAwaitable<string> result) {
            string email = await result;
            await context.PostAsync($"Thanks. Ill add {email} to our mailing list!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("LinkSocialMedia")]
        public async Task LinkSocial(IDialogContext context, LuisResult result) {
            await context.PostAsync("Facebook Account : https://www.facebook.com/1point3creative/");
            await context.PostAsync("Twitter Account : https://twitter.com/1point3creative");
            context.Wait(MessageReceived);
        }

        public RootDialogALuis() {

        }

        public RootDialogALuis(ILuisService service) : base(service) {

        }
    }
}