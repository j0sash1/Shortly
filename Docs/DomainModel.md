# Modelo de Dominio - Shortly

```mermaid
classDiagram

class User {
    +long Id
    +string Email
    +string Password
}

class Link {
    +long Id
    +string Url
    +string ShortUrl
    +int Clicks
    +long UserId
    +IncrementClicks()
}

User "1" --> "*" Link : owns
```