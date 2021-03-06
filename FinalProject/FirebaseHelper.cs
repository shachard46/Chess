using System;
using System.Collections.Generic;
using Android.App;
using Android.Gms.Tasks;
using Firebase;
using Firebase.Firestore;
using Java.Interop;

namespace FinalProject
{
    public class FirebaseHelper :Java.Lang.Object, IOnSuccessListener
    {

        List<Position> positions = new List<Position>();

        readonly FirebaseFirestore db;
        private Activity context;
        private bool disposedValue;

        public FirebaseHelper(Activity context)
        {
            this.context = context;
            var options = new FirebaseOptions.Builder()
            .SetProjectId("chess-6b48d")
            .SetApplicationId("chess-6b48d")
            .SetApiKey("AIzaSyBhO5XaySaLwBKs8KZPlMBNo6OfoBOpnS0")
            .SetStorageBucket("chess-6b48d.appspot.com").Build();

            var app = FirebaseApp.InitializeApp(context, options);

            db = FirebaseFirestore.GetInstance(app);
        }

        public void FetchPositions()
            {
            db.Collection("game").Get().AddOnSuccessListener(context, this);
        }

        public void SendPositions(List<Position> positions)
        {
        }

        public void Finalized()
        {
            
        }

        public List<Position> GetPositions()
        {
            return positions;
        }

        public void Disposed()
        {
            
        }

        public void DisposeUnlessReferenced()
        {
            
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var game = (QuerySnapshot)result;
            foreach (DocumentSnapshot document in game.Documents)
            {
                Position pos = new Position(
                    document.GetString("piece"),
                    Convert.ToInt32(document.GetLong("x")),
                    Convert.ToInt32(document.GetDouble("y")));
                positions.Add(pos);
            }
        }

        public void SetJniIdentityHashCode(int value)
        {
        }

        public void SetJniManagedPeerState(JniManagedPeerStates value)
        {
        }

        public void SetPeerReference(JniObjectReference reference)
        {
        }

        public void UnregisterFromRuntime()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FirebaseHelper()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        public IntPtr Handle;

        public int JniIdentityHashCode;

        public JniObjectReference PeerReference;

        public JniPeerMembers JniPeerMembers;

        public JniManagedPeerStates JniManagedPeerState;
    }
}
