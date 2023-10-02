using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Windows.Controls;

namespace MVPVM
{
    public class BasePresenter<TView, TViewModel> : IPresenter where TView : UserControl
    {

        TView _view;
        TViewModel _viewModel;

        string _region = "Shell";

        public BasePresenter()
        {
            View = (TView)Activator.CreateInstance<TView>();
        }


        IUnityContainer _unityContainer;
        [Dependency]
        public IUnityContainer UnityContainer
        {
            get { return _unityContainer; }
            set
            {
                _unityContainer = value;
            }
        }

        IRegionManager _regionManager;
        [Dependency]
        public IRegionManager RegionManager
        {
            get { return _regionManager; }
            set
            {
                _regionManager = value;
            }
        }


        public TView View
        {
            get { return _view; }
            set
            {
                _view = value;
                OnViewSet();
            }
        }

        public TViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnViewModelSet();
            }
        }


        public Control GetView()
        {
            return this.View;
        }

        protected virtual void OnViewSet()
        {
            ViewModel = (TViewModel)View.DataContext;
        }

        protected virtual void OnViewModelSet()
        {

        }

        protected virtual void OnEventAggregatorSet()
        {

        }
    }
}
