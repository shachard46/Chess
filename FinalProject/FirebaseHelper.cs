using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace FinalProject
{
    public class FirebaseHelper
    {
        private static FirebaseHelper instance = new FirebaseHelper();

        readonly FirestoreDb db;
        public FirebaseHelper()
        {
            db = FirestoreDb.Create("");
        }
        public FirebaseHelper GetInstance()
        {
            return instance;
        }
        public async Task<List<Position>> GetPositions()
        {
            List<Position> positions = new List<Position>();
            CollectionReference collection = db.Collection("cities");

            QuerySnapshot game = await collection.GetSnapshotAsync();

            foreach (DocumentSnapshot document in game.Documents)
            {
                positions.Add(document.ConvertTo<Position>());
            }
            return positions;
        }
    }
}
