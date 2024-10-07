﻿namespace Discordfs.Rest.Resources

open Discordfs.Rest.Common
open Discordfs.Rest.Types
open Discordfs.Types
open System.Collections.Generic
open System.Net
open System.Net.Http
open System.Threading.Tasks

type CreateGlobalApplicationCommand (
    name:                        string,
    ?name_localizations:         IDictionary<string, string> option,
    ?description:                string,
    ?description_localizations:  IDictionary<string, string> option,
    ?options:                    ApplicationCommandOption list,
    ?default_member_permissions: string option,
    ?integration_types:          ApplicationIntegrationType list,
    ?contexts:                   InteractionContextType list,
    ?``type``:                   ApplicationCommandType,
    ?nsfw:                       bool
) =
    inherit Payload() with
        override _.Content = json {
            required "name" name
            optional "name_localizations" name_localizations
            optional "description" description
            optional "description_localizations" description_localizations
            optional "options" options
            optional "default_member_permissions" default_member_permissions
            optional "integration_types" integration_types
            optional "contexts" contexts
            optional "type" ``type``
            optional "nsfw" nsfw
        }

type GetGlobalApplicationCommandResponse =
    | Ok of ApplicationCommand
    | NotFound
    | Other of HttpStatusCode

type EditGlobalApplicationCommand (
    ?name:                       string,
    ?name_localizations:         IDictionary<string, string> option,
    ?description:                string,
    ?description_localizations:  IDictionary<string, string> option,
    ?options:                    ApplicationCommandOption list,
    ?default_member_permissions: string option,
    ?integration_types:          ApplicationIntegrationType list,
    ?contexts:                   InteractionContextType list,
    ?nsfw:                       bool
) =
    inherit Payload() with
        override _.Content = json {
            optional "name" name
            optional "name_localizations" name_localizations
            optional "description" description
            optional "description_localizations" description_localizations
            optional "options" options
            optional "default_member_permissions" default_member_permissions
            optional "integration_types" integration_types
            optional "contexts" contexts
            optional "nsfw" nsfw
        }

type BulkOverwriteGlobalApplicationCommands (
    commands: BulkOverwriteApplicationCommand list // TODO: Confirm this type matches (not documented)
) =
    inherit Payload() with
        override _.Content =
            JsonListPayload commands

type CreateGuildApplicationCommand (
    name:                        string,
    ?name_localizations:         IDictionary<string, string> option,
    ?description:                string,
    ?description_localizations:  IDictionary<string, string> option,
    ?options:                    ApplicationCommandOption list,
    ?default_member_permissions: string option,
    ?``type``:                   ApplicationCommandType,
    ?nsfw:                       bool
) =
    inherit Payload() with
        override _.Content = json {
            required "name" name
            optional "name_localizations" name_localizations
            optional "description" description
            optional "description_localizations" description_localizations
            optional "options" options
            optional "default_member_permissions" default_member_permissions
            optional "type" ``type``
            optional "nsfw" nsfw
        }

type EditGuildApplicationCommand (
    ?name:                       string,
    ?name_localizations:         IDictionary<string, string> option,
    ?description:                string,
    ?description_localizations:  IDictionary<string, string> option,
    ?options:                    ApplicationCommandOption list,
    ?default_member_permissions: string option,
    ?nsfw:                       bool
) =
    inherit Payload() with
        override _.Content = json {
            optional "name" name
            optional "name_localizations" name_localizations
            optional "description" description
            optional "description_localizations" description_localizations
            optional "options" options
            optional "default_member_permissions" default_member_permissions
            optional "nsfw" nsfw
        }

type BulkOverwriteGuildApplicationCommands (
    commands: BulkOverwriteApplicationCommand list
) =
    inherit Payload() with
        override _.Content =
            JsonListPayload commands

type EditApplicationCommandPermissions (
    permissions: ApplicationCommandPermission list
) =
    inherit Payload() with
        override _.Content = json {
            required "permissions" permissions
        }

type IApplicationCommandResource =
    // https://discord.com/developers/docs/interactions/application-commands#get-global-application-commands
    abstract member GetGlobalApplicationCommands:
        applicationId: string ->
        withLocalizations: bool option ->
        Task<ApplicationCommand list>

    // https://discord.com/developers/docs/interactions/application-commands#create-global-application-command
    abstract member CreateGlobalApplicationCommand:
        applicationId: string ->
        content: CreateGlobalApplicationCommand ->
        Task<ApplicationCommand>

    // https://discord.com/developers/docs/interactions/application-commands#get-global-application-command
    abstract member GetGlobalApplicationCommand:
        applicationId: string ->
        commandId: string ->
        Task<GetGlobalApplicationCommandResponse>

    // https://discord.com/developers/docs/interactions/application-commands#edit-global-application-command
    abstract member EditGlobalApplicationCommand:
        applicationId: string ->
        commandId: string ->
        content: EditGlobalApplicationCommand ->
        Task<ApplicationCommand>

    // https://discord.com/developers/docs/interactions/application-commands#delete-global-application-command
    abstract member DeleteGlobalApplicationCommand:
        applicationId: string ->
        commandId: string ->
        Task<unit>

    // https://discord.com/developers/docs/interactions/application-commands#bulk-overwrite-global-application-commands
    abstract member BulkOverwriteGlobalApplicationCommands:
        applicationId: string -> 
        content: BulkOverwriteGlobalApplicationCommands ->
        Task<ApplicationCommand list>

    // https://discord.com/developers/docs/interactions/application-commands#get-guild-application-commands
    abstract member GetGuildApplicationCommands:
        applicationId: string ->
        guildId: string ->
        withLocalizations: bool option ->
        Task<ApplicationCommand list>

    // https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command
    abstract member CreateGuildApplicationCommand:
        applicationId: string ->
        guildId: string ->
        content: CreateGuildApplicationCommand ->
        Task<ApplicationCommand>

    // https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command
    abstract member GetGuildApplicationCommand:
        applicationId: string ->
        guildId: string ->
        commandId: string ->
        Task<ApplicationCommand>

    // https://discord.com/developers/docs/interactions/application-commands#edit-guild-application-command
    abstract member EditGuildApplicationCommand:
        applicationId: string ->
        guildId: string ->
        commandId: string ->
        content: EditGuildApplicationCommand ->
        Task<ApplicationCommand>

    // https://discord.com/developers/docs/interactions/application-commands#delete-guild-application-command
    abstract member DeleteGuildApplicationCommand:
        applicationId: string ->
        guildId: string ->
        commandId: string ->
        Task<unit>

    // https://discord.com/developers/docs/interactions/application-commands#bulk-overwrite-guild-application-commands
    abstract member BulkOverwriteGuildApplicationCommands:
        applicationId: string ->
        guildId: string ->
        content: BulkOverwriteGuildApplicationCommands ->
        Task<ApplicationCommand list>

    // https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command-permissions
    abstract member GetGuildApplicationCommandsPermissions:
        applicationId: string ->
        guildId: string ->
        Task<GuildApplicationCommandPermissions list>

    // https://discord.com/developers/docs/interactions/application-commands#get-application-command-permissions
    abstract member GetGuildApplicationCommandPermissions:
        applicationId: string ->
        guildId: string ->
        commandId: string ->
        Task<GuildApplicationCommandPermissions>

    // https://discord.com/developers/docs/interactions/application-commands#edit-application-command-permissions
    abstract member EditApplicationCommandPermissions:
        applicationId: string ->
        guildId: string ->
        commandId: string ->
        oauth2AccessToken: string ->
        content: EditApplicationCommandPermissions ->
        Task<GuildApplicationCommandPermissions>

type ApplicationCommandResource (httpClientFactory, token) =
    interface IApplicationCommandResource with
        member _.GetGlobalApplicationCommands applicationId withLocalizations =
            req {
                get $"applications/{applicationId}/commands"
                bot token
                query "with_localizations" (withLocalizations >>. _.ToString())
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.CreateGlobalApplicationCommand applicationId content =
            req {
                post $"applications/{applicationId}/commands"
                bot token
                payload content
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.GetGlobalApplicationCommand applicationId commandId =
            req {
                get $"applications/{applicationId}/commands/{commandId}"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.mapT (fun res -> task {
                match res.StatusCode with
                | HttpStatusCode.OK ->
                    let! data = Http.toJson<ApplicationCommand> res
                    return GetGlobalApplicationCommandResponse.Ok data
                | HttpStatusCode.NotFound ->
                    return GetGlobalApplicationCommandResponse.NotFound
                | status ->
                    return GetGlobalApplicationCommandResponse.Other status
            })

        member _.EditGlobalApplicationCommand applicationId commandId content =
            req {
                patch $"applications/{applicationId}/commands/{commandId}"
                bot token
                payload content
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.DeleteGlobalApplicationCommand applicationId commandId =
            req {
                delete $"applications/{applicationId}/commands/{commandId}"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.wait

        member _.BulkOverwriteGlobalApplicationCommands applicationId content =
            req {
                put $"applications/{applicationId}/commands"
                bot token
                payload content
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.GetGuildApplicationCommands applicationId guildId withLocalizations =
            req {
                get $"applications/{applicationId}/guilds/{guildId}/commands"
                bot token
                query "with_localizations" (withLocalizations >>. _.ToString())
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.CreateGuildApplicationCommand applicationId guildId content =
            req {
                post $"applications/{applicationId}/guilds/{guildId}/commands"
                bot token
                payload content
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.GetGuildApplicationCommand applicationId guildId commandId =
            req {
                get $"applications/{applicationId}/guilds/{guildId}/commands/{commandId}"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.EditGuildApplicationCommand applicationId guildId commandId content =
            req {
                patch $"applications/{applicationId}/guilds/{guildId}/commands/{commandId}"
                bot token
                payload content
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.DeleteGuildApplicationCommand applicationId guildId commandId =
            req {
                delete $"applications/{applicationId}/guilds/{guildId}/commands/{commandId}"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.wait

        member _.BulkOverwriteGuildApplicationCommands applicationId guildId content =
            req {
                put $"applications/{applicationId}/guilds/{guildId}/commands"
                bot token
                payload content
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.GetGuildApplicationCommandsPermissions applicationId guildId =
            req {
                get $"applications/{applicationId}/guilds/{guildId}/commands/permissions"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.GetGuildApplicationCommandPermissions applicationId guildId commandId =
            req {
                get $"applications/{applicationId}/guilds/{guildId}/commands/{commandId}/permissions"
                bot token
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson

        member _.EditApplicationCommandPermissions applicationId guildId commandId oauth2AccessToken content =
            req {
                put $"applications/{applicationId}/guilds/{guildId}/commands/{commandId}/permissions"
                oauth oauth2AccessToken
                payload content
            }
            |> Http.send httpClientFactory
            |> Task.mapT Http.toJson
            
// ----- BEGIN TESTING STUFF -----
type RequestContext = {
    Factory: IHttpClientFactory
    Token: string
}

type GetGlobalApplicationCommandResponse2 =
    | Ok of ApplicationCommand
    | NotFound
    | Other of HttpStatusCode

module ApplicationCommands =
    let GetGlobalApplicationCommand (applicationId: string) (commandId: string) (ctx: RequestContext) =
        req {
            get $"applications/{applicationId}/commands/{commandId}"
            bot ctx.Token
        }
        |> Http.send ctx.Factory
        |> Task.mapT (fun res -> task {
            match res.StatusCode with
            | HttpStatusCode.OK ->
                let! data = Http.toJson<ApplicationCommand> res
                return GetGlobalApplicationCommandResponse2.Ok data
            | HttpStatusCode.NotFound ->
                return GetGlobalApplicationCommandResponse2.NotFound
            | status ->
                return GetGlobalApplicationCommandResponse2.Other status
        })

module Example =
    let Test (ctx: RequestContext) = task {
        let! res = ctx |> ApplicationCommands.GetGlobalApplicationCommand "applicationId" "commandId"

        return
            match res with
            | Ok ac -> Some ac
            | NotFound -> None
            | Other _ -> None
    }
// ----- END TESTING STUFF -----
