﻿using System;
using WebFormsMvp.FeatureDemos.Logic.Views;

namespace WebFormsMvp.FeatureDemos.Logic.Presenters
{
    public class SharedPresenter
        : Presenter<ISharedPresenterView>
    {
        public SharedPresenter(ISharedPresenterView view)
            : base(view)
        {
            View.Load += View_Load;
        }

        public override void ReleaseView()
        {
            View.Load -= View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            View.Model.Message = string.Format(@"Presenter instance: {0}",
                Guid.NewGuid());
        }
    }
}