Based on the provided database schema, here are the permissions for each role:

### Customer/Normal User:

- view_products: Allows browsing and viewing available products.
- add_to_cart: Enables adding products to the shopping cart.
- place_order: Grants the ability to place orders for products.
- manage_account: Allows users to view and modify their account details.

### Manager:

- add_product: Permission to add new products to the inventory.
- edit_product: Permission to edit existing product details.
- delete_product: Permission to delete products from the inventory.
- manage_orders: Grants access to view, modify, and manage orders.
- view_payments: Allows viewing payment details.
- update_order_status: Permission to update the status of orders.

### Admin:

- manage_users: Allows adding, editing, and deleting user accounts.
- manage_products: Enables adding, editing, and deleting products from the inventory.
- manage_orders: Grants the ability to view, modify, and manage orders.
- manage_payments: Allows managing payment details, such as viewing, editing, or deleting payments.
- manage_settings: Allows modifying system settings and configurations.
- view_reports: Permission to access and generate reports on user activity, sales, etc.
- update_inventory: Permission to update inventory levels for products.
- manage_discounts: Permission to manage discounts, including adding, editing, or deleting discounts.
- manage_categories: Allows managing product categories, such as adding, editing, or deleting categories.

These permissions should be assigned to respective roles based on the responsibilities and access levels required for each role within the e-commerce system. Adjustments can be made based on specific business requirements and security considerations.
