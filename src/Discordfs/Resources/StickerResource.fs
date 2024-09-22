﻿namespace Modkit.Discordfs.Resources

open Modkit.Discordfs.Common
open Modkit.Discordfs.Types
open Modkit.Discordfs.Utils
open System.Threading.Tasks

type ModifyGuildSticker (
    name:        string,
    description: string option,
    tags:        string
) =
    inherit Payload(Json) with
        override _.Serialize () = json {
            required "name" name
            required "description" description
            required "tags" tags
        }

type IStickerResource =
    // https://discord.com/developers/docs/resources/sticker#list-sticker-packs
    abstract member ListStickerPacks:
        unit ->
        Task<ListStickerPacksResponse>

    // https://discord.com/developers/docs/resources/sticker#get-sticker-pack
    abstract member GetStickerPack:
        packId: string ->
        Task<StickerPack>

    // https://discord.com/developers/docs/resources/sticker#list-guild-stickers
    abstract member ListGuildStickers:
        guildId: string ->
        Task<Sticker list>

    // https://discord.com/developers/docs/resources/sticker#get-guild-sticker
    abstract member GetGuildSticker:
        guildId: string ->
        stickerId: string ->
        Task<Sticker>

    // https://discord.com/developers/docs/resources/sticker#create-guild-sticker
    // TODO: Implement CreateGuildSticker (requires form payload)

    // https://discord.com/developers/docs/resources/sticker#modify-guild-sticker
    abstract member ModifyGuildSticker:
        guildId: string ->
        stickerId: string ->
        auditLogReason: string option ->
        content: ModifyGuildSticker ->
        Task<Sticker>

    // https://discord.com/developers/docs/resources/sticker#delete-guild-sticker
    abstract member DeleteGuildSticker:
        guildId: string ->
        stickerId: string ->
        auditLogReason: string option ->
        Task<unit>

type StickerResource (httpClientFactory, token) =
    interface IStickerResource with
        member _.ListStickerPacks () =
            req {
                get "sticker-packs"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.GetStickerPack packId =
            req {
                get $"sticker-packs/{packId}"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.ListGuildStickers guildId =
            req {
                get $"guilds/{guildId}/stickers"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.GetGuildSticker guildId stickerId =
            req {
                get $"guilds/{guildId}/stickers/{stickerId}"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.ModifyGuildSticker guildId stickerId auditLogReason content =
            req {
                patch $"guilds/{guildId}/stickers/{stickerId}"
                bot token
                audit auditLogReason
                payload content
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.DeleteGuildSticker guildId stickerId auditLogReason =
            req {
                delete $"guilds/{guildId}/stickers/{stickerId}"
                bot token
                audit auditLogReason
            }
            |> Http.send httpClientFactory
            |> Task.wait
