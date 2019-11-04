using FormsControls.Base;

namespace UtMobileApp.Extensions
{
    public partial class InterfaceImplementedPage : Xamarin.Forms.ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new FlipPageAnimation { Duration = AnimationDuration.Long, Subtype = AnimationSubtype.FromTop };

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
            DisplayAlert("title", "started", "ok");
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
            DisplayAlert("title", "finished", "ok");
        }
    }
}
