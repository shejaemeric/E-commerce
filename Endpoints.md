### User Endpoints:

1. **Authentication**:

   - `POST /api/auth/login`: Endpoint to log in users.
   - `POST /api/auth/register`: Endpoint to register new users.
   - `POST /api/auth/logout`: Endpoint to log out users.
   - `POST /api/auth/reset-password`: Endpoint to request a password reset.
   - `POST /api/auth/change-password`: Endpoint to change user's password.

2. **User Profile**:

   - `GET /api/users/{id}`: Endpoint to get user profile by ID.
   - `PUT /api/users/{id}`: Endpoint to update user profile.

3. **Product Listing**:

   - `GET /api/products`: Endpoint to get all products.
   - `GET /api/products/{id}`: Endpoint to get product details by ID.

4. **Order Management**:

   - `GET /api/orders`: Endpoint to get user's orders.
   - `GET /api/orders/{id}`: Endpoint to get order details by ID.
   - `POST /api/orders`: Endpoint to place a new order.
   - `PUT /api/orders/{id}`: Endpoint to update order details.

5. **Cart Management**:
   - `GET /api/cart`: Endpoint to get user's cart items.
   - `POST /api/cart/add`: Endpoint to add a product to the user's cart.
   - `PUT /api/cart/update/{id}`: Endpoint to update quantity of a product in the cart.
   - `DELETE /api/cart/remove/{id}`: Endpoint to remove a product from the cart.

### Manager Endpoints:

1. **Product Management**:

   - `GET /api/manager/products`: Endpoint to get all products.
   - `GET /api/manager/products/{id}`: Endpoint to get product details by ID.
   - `POST /api/manager/products`: Endpoint to add a new product.
   - `PUT /api/manager/products/{id}`: Endpoint to update product details.
   - `DELETE /api/manager/products/{id}`: Endpoint to delete a product.

2. **Order Management**:

   - `GET /api/manager/orders`: Endpoint to get all orders.
   - `GET /api/manager/orders/{id}`: Endpoint to get order details by ID.
   - `PUT /api/manager/orders/{id}`: Endpoint to update order status.

3. **Report Generation**:
   - `GET /api/manager/reports/sales`: Endpoint to generate sales report.
   - `GET /api/manager/reports/inventory`: Endpoint to generate inventory report.
   - `GET /api/manager/reports/user-activity`: Endpoint to generate user activity report.

### Admin Endpoints:

1. **User Management**:

   - `GET /api/admin/users`: Endpoint to get all users.
   - `GET /api/admin/users/{id}`: Endpoint to get user details by ID.
   - `POST /api/admin/users`: Endpoint to add a new user.
   - `PUT /api/admin/users/{id}`: Endpoint to update user details.
   - `DELETE /api/admin/users/{id}`: Endpoint to delete a user.

2. **Discount Management**:

   - `GET /api/admin/discounts`: Endpoint to get all discounts.
   - `GET /api/admin/discounts/{id}`: Endpoint to get discount details by ID.
   - `POST /api/admin/discounts`: Endpoint to add a new discount.
   - `PUT /api/admin/discounts/{id}`: Endpoint to update discount details.
   - `DELETE /api/admin/discounts/{id}`: Endpoint to delete a discount.

3. **Category Management**:
   - `GET /api/admin/categories`: Endpoint to get all categories.
   - `GET /api/admin/categories/{id}`: Endpoint to get category details by ID.
   - `POST /api/admin/categories`: Endpoint to add a new category.
   - `PUT /api/admin/categories/{id}`: Endpoint to update category details.
   - `DELETE /api/admin/categories/{id}`: Endpoint to delete a category.
