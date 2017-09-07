using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

#pragma warning disable 1998

namespace Farm_Bot.Dialogs
{
    [LuisModel("6b7b0ad8-5b3b-425a-bacc-08c79228c0f1", "3a452adbffff45209376addda3e04c1d", domain: "https://westeurope.api.cognitive.microsoft.com/luis/v2.0/apps/6b7b0ad8-5b3b-425a-bacc-08c79228c0f1", staging: true)]
    [Serializable]
    public class RootDialogALuis : LuisDialog<object> {

        [LuisIntent("LearnMore")]
        public async Task GiveInfo(IDialogContext context, LuisResult result) {
            await context.PostAsync("You Can Find Out More On Our Website, Located At : https://www.1point3creative.com");
            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task none(IDialogContext context, LuisResult result) {
            await context.PostAsync("Sorry we didnt understand your message, please try again.");
            context.Wait(MessageReceived);
        }
    }    
}