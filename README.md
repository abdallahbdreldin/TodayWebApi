# TodayWebApi

## Project Overview
TodayWebApi is a backend system built using **ASP.NET Core** for managing an e-commerce platform. It provides RESTful APIs for handling **user authentication, product management, order processing, and tracking**. The backend also integrates **Redis caching** for improved performance and uses **SendGrid for email notifications**.

### Key Features
- **User Authentication:** JWT-based authentication and role management.
- **Product Management:** CRUD operations for products.
- **Order Processing:** Users can place and track their orders.
- **Caching:** Redis is used to cache order details.
- **Payment Integration:** Secure payment processing using Stripe
- **Email Notifications:** Order status updates are sent using SendGrid.
- **Swagger Documentation:** API documentation available via Swagger UI.

---

## Setup Instructions
### Prerequisites
Ensure you have the following installed:
- .NET SDK 7.0 or later
- SQL Server
- Redis (for caching)
- Postman or any API testing tool (optional)

### Installation Steps
1. **Clone the repository:**
   ```sh
   git clone https://github.com/abdallahbdreldin/TodayWebApi.git
   cd TodayWebApi
   ```

2. **Install dependencies:**
   ```sh
   dotnet restore
   ```

3. **Configure the database:**
   - Update `appsettings.json` with your SQL Server connection string:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=TodayWebApiDb;Trusted_Connection=True;"
   }
   ```
   - Run migrations:
   ```sh
   dotnet ef database update
   ```

4. **Set up Redis:**
   Ensure Redis is running and update `appsettings.json` accordingly:
   ```json
   "Redis": {
       "ConnectionString": "localhost"
   }
   ```

5. **Set up SendGrid:**
   - Get an API Key from [SendGrid](https://sendgrid.com/)
   - Add it to `appsettings.json`:
   ```json
   "EmailSettings": {
       "ApiKey": "YOUR_SENDGRID_API_KEY"
   }
   ```

6. **Run the API:**
   ```sh
   dotnet run
   ```
   The API should now be running at `http://localhost:5000;https://localhost:7055`.

---

## API Documentation
Swagger is available at:
- **[Swagger UI](https://localhost:7055/swagger)** (when running locally)

### Sample Endpoints
#### Authentication
- **Login:** `POST /api/auth/login`
- **Register:** `POST /api/auth/register`

#### Products
- **Get all products:** `GET /api/products`
- **Get product by ID:** `GET /api/products/{id}`

#### Orders
- **Place an order:** `POST /api/orders`
- **Get order status:** `GET /api/orders/{id}`


---

## Additional Documentation
### Design Decisions
- **Layered Architecture:** The project follows an **N-Tier architecture** for better separation of concerns.
- **Dependency Injection:** Used throughout the project for better maintainability.
- **JWT Authentication:** Ensures secure user authentication.

### Challenges Encountered

- **Caching Strategy:** Decided to use Redis for performance improvements in order tracking.
- **Payment Integration:** Implementing secure payments using Stripe.
- **Email Notifications:** Ensuring reliable email delivery using SendGrid when order status changes.

### Advanced Features
- **Caching:** Redis is used to cache order statuses, reducing database load.
- **Payment Integration:** Secure payment processing with Stripe.
- **Email Notifications:** Automated email updates sent via SendGrid when an order status is updated


---

## Contributions
Pull requests are welcome! Please ensure your changes include tests where applicable.

For major changes, please open an issue first to discuss what you’d like to change.

---


