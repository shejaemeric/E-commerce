### Cascade Deletes:

- **order_details**:

  - If an order is deleted, its details should also be deleted. Therefore, `order_id` in `order_details` should have a cascading delete relationship with `id` in `orders`.

- **order_items**:

  - If an order is deleted, its associated items should also be deleted. Therefore, `order_id` in `order_items` should have a cascading delete relationship with `id` in `orders`.

- **user_addresses**:

  - If a user is deleted, their associated addresses should also be deleted. Therefore, `user_id` in `user_addresses` should have a cascading delete relationship with `id` in `users`.

- **user_payments**:

  - If a user is deleted, their associated payment details should also be deleted. Therefore, `user_id` in `user_payments` should have a cascading delete relationship with `id` in `users`.

- **password_reset_tokens**:
  - If a user is deleted, any associated password reset tokens should also be deleted. Therefore, `user_id` in `password_reset_tokens` should have a cascading delete relationship with `id` in `users`.

### Cascade Updates:

- None of the relationships seem to require cascading updates based on the provided schema.

### Simple Deletes/Updates:

- **product_categories**:

  - If a category is deleted or updated, you may not want to automatically delete or update associated products. Instead, you might want to handle these changes manually.

- **products**:

  - If a category or discount is deleted or updated, you might not want to automatically delete or update associated products. Instead, you might want to handle these changes manually.

- **cart_items**:

  - If a product or shopping session is deleted, you may want to handle the deletion of associated cart items manually, depending on your business logic.

- **shopping_sessions**:

  - If a session is deleted, you may want to handle the deletion of associated cart items manually, depending on your business logic.

- **audit_logs**:
  - The cascading delete here depends on whether you want to retain audit logs after a user is deleted. If you want to keep the logs for historical purposes, you shouldn't use cascading delete.
