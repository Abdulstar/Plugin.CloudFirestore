﻿using System;
using System.Threading.Tasks;
using Firebase.CloudFirestore;

namespace Plugin.CloudFirestore
{
    public class WriteBatchWrapper : IWriteBatch
    {
        private readonly WriteBatch _writeBatch;

        public WriteBatchWrapper(WriteBatch writeBatch)
        {
            _writeBatch = writeBatch;
        }

        public void Commit(CompletionHandler handler)
        {
            _writeBatch.Commit((error) =>
            {
                handler?.Invoke(error == null ? null : ExceptionMapper.Map(error));
            });
        }

        public Task CommitAsync()
        {
            var tcs = new TaskCompletionSource<bool>();

            _writeBatch.Commit((error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public void SetData(IDocumentReference document, object documentData)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _writeBatch.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper);
        }

        public void SetData(IDocumentReference document, object documentData, params string[] mergeFields)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _writeBatch.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper, mergeFields);
        }

        public void SetData(IDocumentReference document, object documentData, bool merge)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _writeBatch.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper, merge);
        }

        public void UpdateData(IDocumentReference document, object fields)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _writeBatch.UpdateData(fields.ToNativeFieldValues(), (DocumentReference)wrapper);
        }

        public void UpdateData(IDocumentReference document, string field, object value, params object[] moreFieldsAndValues)
        {
            var fields = Field.CreateFields(field, value, moreFieldsAndValues);
            var wrapper = (DocumentReferenceWrapper)document;
            _writeBatch.UpdateData(fields, (DocumentReference)wrapper);
        }

        public void DeleteDocument(IDocumentReference document)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _writeBatch.DeleteDocument((DocumentReference)wrapper);
        }
    }
}
