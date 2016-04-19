using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Interfaces;
using SGAM.Elfec.Presenters.Presentation.Collections;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SGAM.Elfec.Presenters
{
    public class AddRulePresenter : BasePresenter<IAddRuleView>
    {
        public AddRulePresenter(IAddRuleView view, Policy policy, Rule rule = null) : base(view)
        {
            Rule = rule == null ? new Rule() : ObjectCloner.Clone(rule);
            LoadEntities();
        }

        #region Private Attributes
        private ObservableCollection<IEntity> _entities;
        private IEntity _entityToAdd;
        private Rule _rule;
        #endregion
        #region Properties
        public ObservableCollection<IEntity> Entities
        {
            get { return _entities; }
            set
            {
                _entities = value;
                RaisePropertyChanged("Entities");
            }
        }

        public IEntity EntityToAdd
        {
            get { return _entityToAdd; }
            set
            {
                _entityToAdd = value;
                RaisePropertyChanged("EntityToAdd");
            }
        }

        public Rule Rule
        {
            get { return _rule; }
            set
            {
                _rule = value;
                RaisePropertyChanged("Rule");
            }
        }

        #endregion

        #region Private Methods

        private void LoadEntities()
        {
            EntitiesManager.GetAllEntities()
                .Subscribe(
                (entities) =>
                {
                    Entities = entities.ToObservableCollectionAsync();
                },
                (error) =>
                {
                    Debug.WriteLine(error.Message);
                    //show error
                });
        }
        #endregion
    }
}
