namespace Netatmo

open NodaTime

type Token = {
    ExpiresIn: int
    AccessToken: string
    RefreshToken: string
}

type CredentialToken= {
    ExpiresIn: int
    AccessToken: string
    RefreshToken: string
    ReceivedAt: Instant
}

type CredentialManager= {
    AccessToken: string
    CredentialToken: CredentialToken
}

type Client = {
        CredentialManager: CredentialManager
    }

[<AutoOpen>]
module CredentialToken =
     let ExpiresAt credentialToken =
         credentialToken.ReceivedAt.Plus (Duration.FromSeconds (int64 credentialToken.ExpiresIn))

    