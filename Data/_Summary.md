## Entity Relationships Explanation

### CartItem

- **ManyToOne**: A `CartItem` can have one `Product`.
- **ManyToOne**: A `CartItem` can be associated with one `ShoppingSession`.

### Inventory

- **OneToMany**: An `Inventory` can contain multiple `Product` entities.

### OrderDetails

- **ManyToOne**: An `OrderDetails` is associated with one `User`.
- **OneToOne**: An `OrderDetails` has one `PaymentDetails`.

### OrderItems

- **ManyToOne**: An `OrderItems` is associated with one `OrderDetails`.
- **ManyToOne**: An `OrderItems` is associated with one `Product`.

### PaymentDetails

- **OneToMany**: A `PaymentDetails` can be associated with multiple `OrderDetails`.

### Product

- **OneToOne**: A `Product` can have one `Discount`.
- **OneToOne**: A `Product` can have one `Inventory`.
- **OneToOne**: A `Product` can belong to one `ProductCategory`.

### ShoppingSession

- **ManyToOne**: A `ShoppingSession` is associated with one `User`.

### User_Address

- **OneToOne**: A `User_Address` is associated with one `User`.

### User_Payment

- **OneToOne**: A `User_Payment` is associated with one `User`.
