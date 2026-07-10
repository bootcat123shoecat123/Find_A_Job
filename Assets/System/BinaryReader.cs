using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

public class BinaryReader
{
    public static InterviewQuestionMaster ReadQuestion(string qID)
    {
        var messagePackResolvers = CompositeResolver.Create(
           MasterMemory.MasterMemoryResolver.Instance,
           StandardResolver.Instance
       );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        var path = "Binary/InterviewQuestionMasterData";
        var asset = Resources.Load<TextAsset>(path);
        var binary = asset.bytes;

        var memoryDatabase = new MasterMemory.MemoryDatabase(binary);
        return memoryDatabase.InterviewQuestionMasterTable.FindByQuestion_ID(qID);
    }
    public static InterviewReactionCardMaster ReadCard(string cID)
    {
        var messagePackResolvers = CompositeResolver.Create(
           MasterMemory.MasterMemoryResolver.Instance,
           StandardResolver.Instance
       );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        var path = "Binary/InterviewReactionCardMasterData";
        var asset = Resources.Load<TextAsset>(path);
        var binary = asset.bytes;

        var memoryDatabase = new MasterMemory.MemoryDatabase(binary);
        return memoryDatabase.InterviewReactionCardMasterTable.FindByCard_ID(cID);
    }
    public static InterviewerMaster ReadInterviewer(string interviewerID)
    {
        var messagePackResolvers = CompositeResolver.Create(
           MasterMemory.MasterMemoryResolver.Instance,
           StandardResolver.Instance
       );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        var path = "Binary/InterviewerMasterData";
        var asset = Resources.Load<TextAsset>(path);
        var binary = asset.bytes;

        var memoryDatabase = new MasterMemory.MemoryDatabase(binary);

        return memoryDatabase.InterviewerMasterTable.FindByInterviewer_ID(interviewerID);
    }
}