namespace SGAM.Elfec.Model.WebServices.JsonContractResolver
{
    public class SnakeCasePropertyNamesContractResolver : DeliminatorSeparatedPropertyNamesContractResolver
    {
        public SnakeCasePropertyNamesContractResolver() : base('_') { }
    }
}
