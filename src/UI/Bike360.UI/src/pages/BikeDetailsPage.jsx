import { useParams, useLoaderData, useNavigate } from "react-router-dom";
import { Link } from 'react-router-dom'
import { FaArrowLeft } from "react-icons/fa";
import { toast } from "react-toastify";

const BikeDetailsPage = ({ deleteBike }) => {
    const { id } = useParams();
    const bike = useLoaderData();
    const navigate = useNavigate();

    const onDeleteClick = (bikeId) => {
        const confirm = window.confirm('Are you sure you want to delete this bike?');

        if(!confirm) return;

        deleteBike(bikeId);

        toast.success('Bike deleted successfully');

        navigate('/bikes');
    };
      
    return (
        <>
        <section>
            <div className="container m-auto py-6 px-6">
                <Link
                to={'/bikes'}
                className="text-indigo-500 hover:text-indigo-600 flex items-center">
                    <FaArrowLeft className="mr-2" /> Back to all bikes
                </Link>
            </div>
        </section>

        <section className="bg-indigo-50">
            <div className="container m-auto py-10 px-6">
                <div className="grid grid-cols-1 md:grid-cols-70/30 w-full gap-6">
                <main>
                    <div
                    className="bg-white p-6 rounded-lg shadow-md text-center md:text-left"
                    >
                    <div className="text-gray-500 mb-4">{bike.type}</div>
                    <h1 className="text-3xl font-bold mb-4">
                        {bike.brand} {bike.model}
                    </h1>
                    <div
                        className="text-gray-500 mb-4 flex align-middle justify-center md:justify-start"
                    >
                        <i
                        className="fa-solid fa-location-dot text-lg text-orange-700 mr-2"
                        ></i>
                        <p className="text-orange-700">Day rent cost: {bike.rentCostPerDay}</p>
                    </div>
                    </div>

                    <div className="bg-white p-6 rounded-lg shadow-md mt-6">
                    <h3 className="text-indigo-800 text-lg font-bold mb-6">
                        Description
                    </h3>

                    <p className="mb-4">
                    {bike.description}
                    </p>
                    
                    <h3 className="text-indigo-800 text-lg font-bold">Color</h3>
                    <p className="mb-4">{bike.color}</p>

                    <h3 className="text-indigo-800 text-lg font-bold">Frame size</h3>
                    <p className="mb-4">{bike.size}</p>
                    
                    
                    </div>
                </main>

            {/* <!-- Sidebar --> */}
                <aside>
                    <div className="bg-white p-6 rounded-lg shadow-md">
                    <h3 className="text-xl font-bold mb-6">Reservation Info</h3>

                    <h2 className="text-2xl">Available days</h2>

                    <p className="my-2">
                        Here will be calender where users can find all dates.
                    </p>

                    <hr className="my-4" />

                    <h3 className="text-xl">Contact Email:</h3>

                    <p className="my-2 bg-indigo-100 p-2 font-bold">
                        contact@bikeshop.com
                    </p>

                    <h3 className="text-xl">Contact Phone:</h3>

                    <p className="my-2 bg-indigo-100 p-2 font-bold">555-555-5555</p>
                    </div>

                    {/* <!-- Manage --> */}
                    <div className="bg-white p-6 rounded-lg shadow-md mt-6">
                    <h3 className="text-xl font-bold mb-6">Manage Bike</h3>
                    <Link
                        to={`/edit-bike/${bike. id}`}
                        className="bg-indigo-500 hover:bg-indigo-600 text-white text-center font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline mt-4 block">
                            Edit
                        </Link>
                    <button
                        onClick={ () => onDeleteClick(bike.id) }
                        className="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline mt-4 block"
                    >
                        Delete
                    </button>
                    </div>
                </aside>
                </div>
            </div>
        </section>
        </>
    );
};

const bikeLoader = async ({ params }) => {
    const res = await fetch(`/api/bikes/${params.id}`);
    const data = await res.json();
    return data;
 }


export { BikeDetailsPage as default, bikeLoader };