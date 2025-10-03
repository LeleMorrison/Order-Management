
# **Info**

## **Microservices Architecture**
![VP71ReCm44Jl_WehkRI7v1EgE0KXf5PGY5LLwiN2Mq9Zx6YzDEBlwmPGA59xtPstCwFTUL98NMkOFpNsh52rt3GMg3y3TqX7nnX2HShM1lXAfk_sBlPR_ldI0VRtAwuybobV2zDKi3STpDKYJtcY22wg3rY66l1PMLL32vQsOJlmYJZ4h-ADhv-Q-aWb0PZbfIlhIy2lO7E5iYAAYt9a](https://github.com/user-attachments/assets/633e8e48-abe6-492e-8bbd-48e850d070b2)

Each Microservice has its own DB (SQL Lite).

## **Entity Diagram**
![ZPFFJi9048VlVOe95_mZHAx4610n8J688WymTaSoeTqrirCICBwBHz-35tDRgisY1qxDVDtvflDhE_2AeaMU9Nx5zWq5SqWpz7weAlnIA4rIP-Uy9RXCaZQChRBkFi4mpCIP8bK_9rbpIadLzaFXFG4uXuc1Fh3LgfXYJg7qGMoN5Pa9foKob7AEMVAHr9OHAYsTR6fDA2DJg6DPQ6p3](https://github.com/user-attachments/assets/f09be162-4706-463e-b19d-a527dae51a88)

# **Requirements**

* **.NET 8 SDK** (or newer) installed.  
* An editor like **Visual Studio 2022** (or VS Code) to run the solution.  
* (Optional) **Git**, if you want to clone the project from a repository.

# **Installation and Startup Instructions**

1. Cloning the repository:
   
   Run the following command to clone the project from GitHub:
   
```bash
git clone https://github.com/LeleMorrison/Order-Management.git
```

# **Package Restoration and Build:**

Open the solution in **Visual Studio** or, from the command line, navigate to the project folder and run:
```bash
dotnet restore  
dotnet build
```

This command will restore all necessary NuGet packages (e.g., Entity Framework Core for SQLite and InMemory, xUnit, etc.) and build all projects.

## **Testing and Execution**

Open Visual Studio and navigate to the **Test** menu, or press **CTRL** \+ **R**\+**A**, and click the green button to proceed with the tests.  
Alternatively, you can test the calls using **Postman**.

![immagine](https://github.com/user-attachments/assets/7c0e578c-fafc-4f8e-9c96-e538e91cb24b)

## **Postman Method**

Launch Visual Studio and start all microservices.  
Example of creating an order with Postman: insert the header **Content-Type** with value **application/json**, where items is an array of products.
```json
{  
  "userId": 1,  
  "addressId": 2,  
  "items":[  
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

If the services have started correctly, you will receive a **201** status code from Postman with this response:
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


