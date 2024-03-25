1. **One-to-Many Relationship:**

   - `Product` and `CartItem`: One product can be associated with multiple cart items, but each cart item can only belong to one product.
   - `Product` and `OrderItems`: Similar to the `CartItem` relationship, one product can be part of multiple orders, but each order item is associated with only one product.
   - `Discount` and `Product`: A discount can be applied to multiple products, but each product can have only one discount.
   - `Inventory` and `Product`: An inventory can contain multiple products, but each product can only be associated with one inventory.
   - `User` and `User_Address`: Each user can have many addresses, and each address belongs to only one user.
   - `User` and `User_Payment`: Similarly, each user can have many payment methods associated with their account,and each payment belongs to only one user.

2. **Many-to-One Relationship:**

   - `CartItem` and `ShoppingSession`: Multiple cart items can belong to a single shopping session, but each cart item belongs to only one session.

3. **One-to-One Relationship:**

   - `PaymentDetails` and `OrderDetails`: Each order has payment details associated with it, and each payment detail is linked to a specific order.

For visual representation, here's how the relationships can be described:

```bash
    Product --> ProductCategory
    Product <-- CartItem --> ShoppingSession
    Product<-- OrderItems --> OrderDetails --> PaymentDetails
    Product<-- Discount
    Product<-- Inventory
    User <-- User_Address
    User<-- User_Payment

```
