using System;
using System.Collections.Generic;
using Amazon.BedrockRuntime;
using OpenAI.Chat;

namespace Gibbon.AI.Models.Chat
{
    public enum Role
    {
        System,
        Assistant,
        User,
        Tool,
        Function
    }
    
    public static class EnumCorrelator
    {
        private static readonly Dictionary<Role, (ChatMessageRole, ConversationRole)> correlations;

        static EnumCorrelator()
        {
            correlations = new Dictionary<Role, (ChatMessageRole, ConversationRole)>
            {
                { Role.System, (ChatMessageRole.System, ConversationRole.User) },
                { Role.Assistant, (ChatMessageRole.Assistant, ConversationRole.Assistant) },
                { Role.User, (ChatMessageRole.User, ConversationRole.User) },
                { Role.Tool, (ChatMessageRole.Tool, ConversationRole.User) },
                { Role.Function, (ChatMessageRole.Function, ConversationRole.User) },
            };
        }

        private static (ChatMessageRole OpenAIRole, ConversationRole AWSBedrockRole) GetCorrelatedValues(Role gibbonRole)
        {
            if (correlations.TryGetValue(gibbonRole, out var result))
            {
                return result;
            }
            throw new ArgumentException($"No correlation found for {gibbonRole}", nameof(gibbonRole));
        }

        public static ChatMessageRole AsOpenAIRole(this Role gibbonRole) => GetCorrelatedValues(gibbonRole).OpenAIRole;
        public static ConversationRole AsAWSBedrockRole(this Role gibbonRole) => GetCorrelatedValues(gibbonRole).AWSBedrockRole;
    }
}