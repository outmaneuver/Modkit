﻿namespace Discordfs.Rest.Common

open System.Collections.Generic
open System.Text.Json

[<System.Obsolete>]
type Dto () =
    member val private Properties: IDictionary<string, obj> = Dictionary()

    /// Add a property to the DTO.
    static member property (key: string) (value: 'a) (dto: Dto) =
        dto.Properties.Add(key, value)
        dto

    /// Add a property to the DTO if the option is some.
    static member propertyIf (key: string) (value: 'a option) (dto: Dto) =
        if value.IsSome then
            dto.Properties.Add(key, value)
        dto

    /// Get the underlying data of the DTO to use as a value of another DTO's property.
    static member object (dto: Dto) =
        dto.Properties

    /// Serialize the DTO into json.
    static member json (dto: Dto) =
        JsonSerializer.Serialize dto.Properties
