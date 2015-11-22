﻿using UIKit;
using CodeHub.iOS.Services;
using System;

namespace CodeHub.iOS.ViewControllers.Walkthrough
{
    public partial class PromoteView : BaseViewController
    {
        private bool _shouldWatch;
        private bool _shouldStar;

        public PromoteView()
            : base("PromoteView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            StarButton.BackgroundColor = UIColor.FromRGB(0x2c, 0x3e, 0x50);
            StarButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            StarButton.Layer.CornerRadius = 6f;

            WatchButton.BackgroundColor = UIColor.FromRGB(0x7f, 0x8c, 0x8d);
            WatchButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            WatchButton.Layer.CornerRadius = 6f;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            WatchButton.TouchUpInside += WatchCodeHub;
            StarButton.TouchUpInside += StarCodeHub;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            WatchButton.TouchUpInside -= WatchCodeHub;
            StarButton.TouchUpInside -= StarCodeHub;
        }

        private void StarCodeHub(object sender, EventArgs args)
        {
            _shouldStar = !_shouldStar;
            DefaultValueService.Instance.Set("SHOULD_STAR_CODEHUB", _shouldStar);
            var color = _shouldStar ? UIColor.FromRGB(0xbd, 0xc3, 0xc7) : UIColor.FromRGB(0x2c, 0x3e, 0x50);
            StarButton.SetTitle(_shouldStar ? "Good Choice!" : "Star", UIControlState.Normal);
            UIView.Animate(0.3f, () => StarButton.BackgroundColor = color);
            Alert();
        }

        private void WatchCodeHub(object sender, EventArgs args)
        {
            _shouldWatch = !_shouldWatch;
            DefaultValueService.Instance.Set("SHOULD_WATCH_CODEHUB", _shouldWatch);
            var color = _shouldWatch ? UIColor.FromRGB(0xbd, 0xc3, 0xc7) : UIColor.FromRGB(0x7f, 0x8c, 0x8d);
            WatchButton.SetTitle(_shouldWatch ? "Very Nice!" : "Watch", UIControlState.Normal);
            UIView.Animate(0.3f, () => WatchButton.BackgroundColor = color);
            Alert();
        }

        private bool _hasAlerted;
        private void Alert()
        {
            if (_hasAlerted)
                return;
            _hasAlerted = true;

            var alert = UIAlertController.Create("Awesome!", "When you login to your account I will make the changes! Thanks for helping create a better CodeHub!", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, x => {}));
            PresentViewController(alert, true, null);
        }
    }
}
