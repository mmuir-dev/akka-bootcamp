namespace AkkaWordCounter2.App.Actors;

public sealed class WordCounterManager : ReceiveActor
{
    public WordCounterManager()
    {
        Receive<IWithDocumentId>(s =>
        {
            string childName = $"word-counter-{s.DocumentId.GetHashCode():X8}";
            IActorRef child = Context.Child(childName);
            if (child.IsNobody())
            {
                //start the child if it doesn't exist
                child = Context.ActorOf(Props.Create(() => 
                new DocumentWordCounter(s.DocumentId)), childName);
            }
            child.Forward(s);
        });
    }
}
