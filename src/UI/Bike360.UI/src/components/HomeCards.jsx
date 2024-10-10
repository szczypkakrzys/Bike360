import Card from "./Card";
import { Link } from 'react-router-dom'

const HomeCards = () => {
    return (
        <section className="py-4">
      <div className="container-xl lg:container m-auto">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4 p-4 rounded-lg">
         <Card>
            <h2 className="text-2xl font-bold">For Customers</h2>
            <p className="mt-2 mb-4">
            Browse our bikes and start your cycling journet today!
            </p>
            <Link
            to="/bikes"
            className="inline-block bg-black text-white rounded-lg px-4 py-2 hover:bg-gray-700"
            >
            Browse Bikes
            </Link>
         </Card>
         <Card bg='bg-indigo-100'>
            <h2 className="text-2xl font-bold">For Employees</h2>
            <p className="mt-2 mb-4">
              Go to employee dashboard
            </p>
            <Link
              to="/employee"
              className="inline-block bg-indigo-500 text-white rounded-lg px-4 py-2 hover:bg-indigo-600"
            >
              Employee Panel
            </Link>
         </Card>
        </div>
      </div>
    </section>
    );
};
export default HomeCards;