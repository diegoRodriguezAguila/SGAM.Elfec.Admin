using SGAM.Elfec.Model;

namespace SGAM.Elfec.Presenters.Views
{
    /// <summary>
    /// Vista de detalles de una directiva de usuario
    /// </summary>
    public interface IPolicyDetailsView : IBaseView
    {
        void AddRule(Policy policy);
    }
}
