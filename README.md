# Factory System Project

This is a simple Factory Project that simulates a factory management system. The project is implemented in C# and provides functionalities to manage products and employees in a factory.

## Features

1. Add and Remove Products: You can add new products to the factory inventory and remove existing products by their barcode.

2. Add and Remove Employees: You can add new workers and supervisors to the factory and remove employees by their names.

3. Promote Worker to Supervisor: You can promote a worker to a supervisor role by providing their ID.

4. Show Products and Employees: You can display the list of products and employees in the factory.

## Usage

1. Clone the repository to your local machine.

2. Open the solution file (Factory-Project.sln) in Visual Studio.

3. Build the solution to compile the project.

4. Run the application to use the factory management system.

5. The application provides a simple command-line interface to interact with the factory management system.

6. Use the following commands to manage the factory:

   - `1`: Add Product
   - `2`: Remove Product by Barcode
   - `3`: Add Worker
   - `4`: Remove Employee by Name
   - `5`: Promote Worker to Supervisor by ID
   - `6`: Show Products
   - `7`: Show Employees
   - `8`: Exit

## Data Persistence

The factory data is saved and loaded from a text file (`factory_data.txt`) in the project's `bin/Debug` folder. The data is stored in a simple comma-separated format for easy loading and parsing.

## Generating Sample Data

The project includes a `GenerateData` method that creates 5 workers, 2 supervisors, and 10 products manually. It then saves the generated data to the `factory_data.txt` file. You can use this method to create sample data to test the application.

## Note

This project is a simple demonstration of a factory management system and may not cover all edge cases or handle extensive error checking. It is meant for educational purposes and can be expanded and improved upon for real-world applications.

## License

This project is licensed under the [MIT License](LICENSE).

Feel free to contribute to the project or use it as a starting point for your own projects. Happy coding!
