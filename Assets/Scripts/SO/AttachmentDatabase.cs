using UnityEngine;

[CreateAssetMenu(fileName = "AttachmentDatabase", menuName = "Attachment/AttachmentDatabase")]
public class AttachmentDatabase : ScriptableObject
{
    [SerializeField] AttachmentDataSO[] attachmentDB;
    public AttachmentDataSO[] AttachmentDB => attachmentDB;
}