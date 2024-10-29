﻿namespace Discordfs.Types

open System
open System.Text.Json
open System.Text.Json.Serialization

#nowarn "49"

// https://discord.com/developers/docs/topics/gateway-events#identify-identify-structure
type IdentifySendEvent = {
    [<JsonPropertyName "token">] Token: string
    [<JsonPropertyName "properties">] Properties: ConnectionProperties
    [<JsonPropertyName "compress">] Compress: bool option
    [<JsonPropertyName "large_threshold">] LargeThreshold: int option
    [<JsonPropertyName "shard">] Shard: (int * int) option
    [<JsonPropertyName "presence">] Presence: UpdatePresenceSendEvent option
    [<JsonPropertyName "intents">] Intents: int
}
with
    static member build(
        Token: string,
        Intents: int,
        Properties: ConnectionProperties,
        ?Compress: bool,
        ?LargeThreshold: int,
        ?Shard: int * int,
        ?Presence: UpdatePresenceSendEvent
    ) = {
        Token = Token;
        Intents = Intents;
        Properties = Properties;
        Compress = Compress;
        LargeThreshold = LargeThreshold;
        Shard = Shard;
        Presence = Presence;
    }                

// https://discord.com/developers/docs/topics/gateway-events#resume-resume-structure
and ResumeSendEvent = {
    [<JsonPropertyName "token">] Token: string
    [<JsonPropertyName "session_id">] SessionId: string
    [<JsonPropertyName "seq">] Sequence: int
}
with
    static member build(
        Token: string,
        SessionId: string,
        Sequence: int
    ) = {
        Token = Token;
        SessionId = SessionId;
        Sequence = Sequence;
    }

// https://discord.com/developers/docs/topics/gateway-events#heartbeat-example-heartbeat
and HeartbeatSendEvent = int option

// https://discord.com/developers/docs/topics/gateway-events#request-guild-members-request-guild-members-structure
and RequestGuildMembersSendEvent = {
    [<JsonPropertyName "guild_id">] GuildId: string
    [<JsonPropertyName "query">] Query: string option
    [<JsonPropertyName "limit">] Limit: int
    [<JsonPropertyName "presences">] Presences: bool option
    [<JsonPropertyName "user_ids">] UserIds: string list option
    [<JsonPropertyName "nonce">] Nonce: string option
}
with
    static member build(
        GuildId: string,
        Limit: int,
        ?Presences: bool,
        ?Query: string,
        ?UserIds: string list,
        ?Nonce: string
    ) = {
        GuildId = GuildId;
        Query = Query;
        Limit = Limit;
        Presences = Presences;
        UserIds = UserIds;
        Nonce = Nonce;
    }

// https://discord.com/developers/docs/topics/gateway-events#update-voice-state-gateway-voice-state-update-structure
and UpdateVoiceStateSendEvent = {
    [<JsonPropertyName "guild_id">] GuildId: string
    [<JsonPropertyName "channel_id">] ChannelId: string option
    [<JsonPropertyName "self_mute">] SelfMute: bool
    [<JsonPropertyName "self_deaf">] SelfDeaf: bool
}
with
    static member build(
        GuildId: string,
        ChannelId: string option,
        SelfMute: bool,
        SelfDeaf: bool
    ) = {
        GuildId = GuildId;
        ChannelId = ChannelId;
        SelfMute = SelfMute;
        SelfDeaf = SelfDeaf;
    }

// https://discord.com/developers/docs/events/gateway-events#request-soundboard-sounds
and RequestSoundboardSoundsSendEvent = {
    [<JsonPropertyName "guild_ids">] GuildIds: string list 
}
with
    static member build(
        GuildIds: string list
    ) = {
        GuildIds = GuildIds;
    }

// https://discord.com/developers/docs/topics/gateway-events#update-presence-gateway-presence-update-structure
and UpdatePresenceSendEvent = {
    [<JsonPropertyName "since">] Since: int option
    [<JsonPropertyName "activities">] Activities: Activity list
    [<JsonPropertyName "status">] Status: StatusType
    [<JsonPropertyName "afk">] Afk: bool
}
with
    static member build(
        Status: StatusType,
        ?Activities: Activity list,
        ?Afk: bool,
        ?Since: int
    ) = {
        Since = Since;
        Activities = Option.defaultValue [] Activities;
        Status = Status;
        Afk = Option.defaultValue false Afk;
    }

// https://discord.com/developers/docs/events/gateway#connection-lifecycle
type HeartbeatReceiveEvent = Empty

// https://discord.com/developers/docs/events/gateway#connection-lifecycle
type HeartbeatAckReceiveEvent = Empty

// https://discord.com/developers/docs/topics/gateway#hello-event-example-hello-event
type HelloReceiveEvent = {
    [<JsonPropertyName "heartbeat_interval">] HeartbeatInterval: int
}

// https://discord.com/developers/docs/topics/gateway-events#ready-ready-event-fields
type ReadyReceiveEvent = {
    [<JsonPropertyName "v">] Version: int
    [<JsonPropertyName "user">] User: User
    [<JsonPropertyName "guilds">] Guilds: UnavailableGuild list
    [<JsonPropertyName "session_id">] SessionId: string
    [<JsonPropertyName "resume_gateway_url">] ResumeGatewayUrl: string
    [<JsonPropertyName "shard">] Shard: (int * int) option
    [<JsonPropertyName "application">] Application: PartialApplication
}

// https://discord.com/developers/docs/events/gateway-events#resumed
type ResumedReceiveEvent = Empty

// https://discord.com/developers/docs/events/gateway-events#reconnect
type ReconnectReceiveEvent = Empty

// https://discord.com/developers/docs/topics/gateway-events#invalid-session
type InvalidSessionReceiveEvent = bool

// https://discord.com/developers/docs/events/gateway-events#application-command-permissions-update
type ApplicationCommandPermissionsUpdateReceiveEvent = ApplicationCommandPermission

// https://discord.com/developers/docs/events/gateway-events#auto-moderation-rule-create
type AutoModerationRuleCreateReceiveEvent = AutoModerationRule

// https://discord.com/developers/docs/events/gateway-events#auto-moderation-rule-update
type AutoModerationRuleUpdateReceiveEvent = AutoModerationRule

// https://discord.com/developers/docs/events/gateway-events#auto-moderation-rule-delete
type AutoModerationRuleDeleteReceiveEvent = AutoModerationRule

// https://discord.com/developers/docs/events/gateway-events#auto-moderation-action-execution-auto-moderation-action-execution-event-fields
type AutoModerationActionExecutionReceiveEvent = {
    [<JsonPropertyName "guild_id">] GuildId: string
    [<JsonPropertyName "action">] Action: AutoModerationAction
    [<JsonPropertyName "rule_id">] RuleId: string
    [<JsonPropertyName "rule_trigger_type">] RuleTriggerType: AutoModerationTriggerType
    [<JsonPropertyName "user_id">] UserId: string
    [<JsonPropertyName "channel_id">] ChannelId: string option
    [<JsonPropertyName "message_id">] MessageId: string option
    [<JsonPropertyName "alert_system_message_id">] AlertSystemMessageId: string option
    [<JsonPropertyName "content">] Content: string // TODO: Consider making option, as doesn't exist if missing message content intent
    [<JsonPropertyName "matched_keyword">] MatchedKeyword: string option
    [<JsonPropertyName "matched_content">] MatchedContent: string option
}

// https://discord.com/developers/docs/events/gateway-events#channel-create
type ChannelCreateReceiveEvent = Channel

// https://discord.com/developers/docs/events/gateway-events#channel-update
type ChannelUpdateReceiveEvent = Channel

// https://discord.com/developers/docs/events/gateway-events#channel-delete
type ChannelDeleteReceiveEvent = Channel

// https://discord.com/developers/docs/events/gateway-events#thread-create
type ThreadCreateReceiveEvent = Channel

// TODO: Can contain `newly_created` or thread member (?) in above

// https://discord.com/developers/docs/events/gateway-events#thread-update
type ThreadUpdateReceiveEvent = Channel

// https://discord.com/developers/docs/events/gateway-events#thread-delete
type ThreadDeleteReceiveEvent = {
    [<JsonPropertyName "id">] Id: string
    [<JsonPropertyName "guild_id">] GuildId: string
    [<JsonPropertyName "parent_id">] ParentId: string
    [<JsonPropertyName "type">] Type: ChannelType
}

// https://discord.com/developers/docs/events/gateway-events#thread-list-sync-thread-list-sync-event-fields
type ThreadListSyncReceiveEvent = {
    [<JsonPropertyName "guild_id">] GuildId: string
    [<JsonPropertyName "channel_ids">] ChannelIds: string list option
    [<JsonPropertyName "threads">] Threads: Channel list
    [<JsonPropertyName "members">] Members: ThreadMember list
}

// https://discord.com/developers/docs/events/gateway-events#thread-member-update
type ThreadMemberUpdateReceiveEvent = ThreadMember

// TODO: Also contains `guild_id` field in above

// https://discord.com/developers/docs/events/gateway-events#thread-members-update
type ThreadMembersUpdateReceiveEvent = {
    [<JsonPropertyName "id">] Id: string
    [<JsonPropertyName "guild_id">] GuildId: string
    [<JsonPropertyName "member_count">] MemberCount: int
    [<JsonPropertyName "added_members">] AddedMembers: ThreadMember list option
    [<JsonPropertyName "removed_member_ids">] RemovedMemberIds: string list option
}

// https://discord.com/developers/docs/events/gateway-events#channel-pins-update-channel-pins-update-event-fields
type ChannelPinsUpdateReceiveEvent = {
    [<JsonPropertyName "guild_id">] GuildId: string option
    [<JsonPropertyName "channel_id">] ChannelIds: string
    [<JsonPropertyName "last_pin_timestamp">] [<JsonConverter(typeof<Converters.UnixEpoch>)>] LastPinTimestamp: DateTime option
}

// https://discord.com/developers/docs/events/gateway-events#entitlement-create
type EntitlementCreateReceiveEvent = Entitlement

// https://discord.com/developers/docs/events/gateway-events#entitlement-update
type EntitlementUpdateReceiveEvent = Entitlement

// https://discord.com/developers/docs/events/gateway-events#entitlement-delete
type EntitlementDeleteReceiveEvent = Entitlement

// https://discord.com/developers/docs/events/gateway-events#guild-create
[<JsonConverter(typeof<GuildCreateConverter>)>]
type GuildCreateReceiveEvent =
    | AvailableGuild of Guild
    | UnavailableGuild of UnavailableGuild

// TODO: AvailableGuild contains many additional properties: https://discord.com/developers/docs/events/gateway-events#guild-create-guild-create-extra-fields

and GuildCreateConverter () =
    inherit JsonConverter<GuildCreateReceiveEvent> ()

    override _.Read (reader, typeToConvert, options) =
        let success, document = JsonDocument.TryParseValue &reader
        if not success then raise (JsonException())

        let available =
            match document.RootElement.TryGetProperty "name" with
            | exists, _ -> exists

        let json = document.RootElement.GetRawText()

        match available with
        | true -> AvailableGuild <| Json.deserializeF json
        | false -> UnavailableGuild <| Json.deserializeF json

    override _.Write (writer, value, options) =
        match value with
        | AvailableGuild a -> Json.serializeF a |> writer.WriteRawValue
        | UnavailableGuild u -> Json.serializeF u |> writer.WriteRawValue

// https://discord.com/developers/docs/events/gateway-events#guild-update
type GuildUpdateReceiveEvent = Guild

// https://discord.com/developers/docs/events/gateway-events#guild-delete
type GuildDeleteReceiveEvent = UnavailableGuild

// TODO: `unavailable` field is not net if bot removed from guild in above

// https://discord.com/developers/docs/events/gateway-events#guild-audit-log-entry-create
type GuildAuditLogEntryCreateReceiveEvent = AuditLogEntry

// TODO: Also contains `guild_id` field in above

// https://discord.com/developers/docs/events/gateway-events#guild-ban-add
type GuildBanAddReceiveEvent = {
    [<JsonPropertyName "guild_id">] GuildId: string
    [<JsonPropertyName "user">] user: User
}

// https://discord.com/developers/docs/events/gateway-events#guild-ban-remove
type GuildBanRemoveReceiveEvent = {
    [<JsonPropertyName "guild_id">] GuildId: string
    [<JsonPropertyName "user">] user: User
}

// IDEA: Serializing additional properties could be done through a custom converter which creates an object containing
//       the main object in one property and the additional metadata in another. This could also be done to things like
//       InviteWithMetdata!!!

// https://discord.com/developers/docs/topics/gateway-events#typing-start-typing-start-event-fields
type TypingStartReceiveEvent = {
    [<JsonPropertyName "channel_id">] ChannelId: string
    [<JsonPropertyName "guild_id">] GuildId: string option
    [<JsonPropertyName "user_id">] UserId: string
    [<JsonPropertyName "timestamp">] Timestamp: DateTime
    [<JsonPropertyName "member">] Member: GuildMember
}
with
    static member build(
        ChannelId: string,
        UserId: string,
        Timestamp: DateTime,
        Member: GuildMember,
        ?GuildId: string
    ) = {
        ChannelId = ChannelId;
        GuildId = GuildId;
        UserId = UserId;
        Timestamp = Timestamp;
        Member = Member;
    }