﻿using System;
using Firebase.Firestore;

namespace Plugin.CloudFirestore
{
    public class SnapshotMetadataWrapper : ISnapshotMetadata
    {
        public bool HasPendingWrites => SnapshotMetadata.HasPendingWrites;

        public bool IsFromCache => SnapshotMetadata.IsFromCache;

        public SnapshotMetadata SnapshotMetadata { get; }

        public SnapshotMetadataWrapper(SnapshotMetadata snapshotMetadata)
        {
            SnapshotMetadata = snapshotMetadata;
        }
    }
}
