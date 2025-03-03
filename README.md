# Info

## Architettura a microservizi.

![VP71ReCm44Jl_WehkRI7v1EgE0KXf5PGY5LLwiN2Mq9Zx6YzDEBlwmPGA59xtPstCwFTUL98NMkOFpNsh52rt3GMg3y3TqX7nnX2HShM1lXAfk_sBlPR_ldI0VRtAwuybobV2zDKi3STpDKYJtcY22wg3rY66l1PMLL32vQsOJlmYJZ4h-ADhv-Q-aWb0PZbfIlhIy2lO7E5iYAAYt9a](https://github.com/user-attachments/assets/633e8e48-abe6-492e-8bbd-48e850d070b2)

ogni Microservizio ha il proprio DB (SQL Lite).

## Diagramma entità

![ZPFFJi9048VlVOe95_mZHAx4610n8J688WymTaSoeTqrirCICBwBHz-35tDRgisY1qxDVDtvflDhE_2AeaMU9Nx5zWq5SqWpz7weAlnIA4rIP-Uy9RXCaZQChRBkFi4mpCIP8bK_9rbpIadLzaFXFG4uXuc1Fh3LgfXYJg7qGMoN5Pa9foKob7AEMVAHr9OHAYsTR6fDA2DJg6DPQ6p3](https://github.com/user-attachments/assets/f09be162-4706-463e-b19d-a527dae51a88)



# Requisiti

- .NET 8 SDK (o superiore) installato.
- Un editor come Visual Studio 2022 (o VS Code) per eseguire la soluzione.
- (Opzionale) Git, se si vuole clonare il progetto da un repository.

# Istruzioni per l'installazione e l'avvio

1. **Clonazione del repository:**  
   Esegui il comando seguente per clonare il progetto da GitHub:

     ```bash
     git clone https://github.com/LeleMorrison/GestioneOrdini-PhotoSi.git
     ```



# Ripristino dei pacchetti e build:
Apri la soluzione in Visual Studio oppure, da linea di comando, posizionati nella cartella del progetto ed esegui:
```bash
dotnet restore
dotnet build
```
Questo comando ripristinerà tutti i pacchetti NuGet necessari (ad esempio, Entity Framework Core per SQLite e InMemory, xUnit etc.) e compilerà tutti i progetti.

## Test ed esecuzione
Aprire visual studio e procedere nel menù test o premere **CRTL** + **R**+**A** e cliccare il tasto verde per procedere nei test.


![immagine](https://github.com/user-attachments/assets/7c0e578c-fafc-4f8e-9c96-e538e91cb24b)

In alternativa si possono testare le chiamate con **PostMan**.

## Metodo Postman
Avviare visual studio e avviare tutti i microservizi.
Esempio creare un ordine con postman, inserire l'header **Content-Type** con value **application/json** dove item è un array di prodotti.

```json
{
  "userId": 1,
  "addressId": 2,
  "items": [
    {
      "productId": 17,
      "quantity": 2
    },
        {
      "productId": 18,
      "quantity": 2
    }
  ]
}
```
Se i servizi sono stati correttamente avviati riceveremo codice **201** da parte di postman con questa response:
```json
{
    "id": 6,
    "userId": 1,
    "addressId": 2,
    "orderDate": "2025-03-03T22:13:59.9211812Z",
    "items": [
        {
            "id": 14,
            "orderId": 6,
            "productId": 17,
            "quantity": 2
        },
        {
            "id": 15,
            "orderId": 6,
            "productId": 18,
            "quantity": 2
        }
    ]
}
```

