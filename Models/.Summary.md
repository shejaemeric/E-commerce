The relationships between your models appear to be mostly correct based on the provided code. Below is a textual representation of how the relationships work:

1. **One-to-Many Relationship:**

   - `Product` and `CartItem`: One product can be associated with multiple cart items, but each cart item can only belong to one product.
   - `Product` and `OrderItems`: Similar to the `CartItem` relationship, one product can be part of multiple orders, but each order item is associated with only one product.
   - `Discount` and `Product`: A discount can be applied to multiple products, but each product can have only one discount.
   - `Inventory` and `Product`: An inventory can contain multiple products, but each product can only be associated with one inventory.

2. **Many-to-One Relationship:**

   - `CartItem` and `ShoppingSession`: Multiple cart items can belong to a single shopping session, but each cart item belongs to only one session.

3. **One-to-One Relationship:**

   - `PaymentDetails` and `OrderDetails`: Each order has payment details associated with it, and each payment detail is linked to a specific order.
   - `User` and `User_Address`: Each user can have only one address, and each address belongs to only one user.
   - `User` and `User_Payment`: Similarly, each user can have only one payment method associated with their account.

4. **Many-to-Many Relationship:**
   - There doesn't seem to be a direct many-to-many relationship defined in the provided models. However, you can potentially create a many-to-many relationship between `Product` and `ProductCategory` if a product can belong to multiple categories and a category can have multiple products.

For visual representation, here's how the relationships can be described:

```bash
     Product <-- CartItem --> ShoppingSession
        <-- OrderItems --> OrderDetails --> PaymentDetails
        <-- Discount
        <-- Inventory

    User <-- User_Address
        <-- User_Payment

    Product --> ProductCategory
```
