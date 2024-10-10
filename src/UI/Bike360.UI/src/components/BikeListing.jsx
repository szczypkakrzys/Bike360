import { useState } from "react";
import { Link } from 'react-router-dom'

const BikeListing = ({ bike }) => {
    const[showFullDescription, setShowFullDescription] = useState(false);
    
    //TODO add description
    let description = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc at suscipit magna. Nunc sit amet felis luctus, aliquet dolor id, viverra nisl. Quisque vitae feugiat risus. Quisque porta rutrum lectus, vel maximus lectus congue sed. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec sagittis venenatis erat nec cursus. Integer nec faucibus nulla, quis ultrices dolor. Aenean vitae fringilla lorem, vel euismod velit. Vestibulum ex felis, sodales et purus ut, accumsan maximus diam. Curabitur eget urna vitae orci dapibus hendrerit. In tincidunt erat in erat ultricies, quis elementum nisi lacinia. Ut venenatis ultrices vestibulum.';//bike.description;

    if(!showFullDescription){
        description = description.substring(0, 90) + '...';
    }

    return(
        <div className="bg-white rounded-xl shadow-md relative">
            <div className="p-4">
            <div className="mb-6">
                <div className="text-gray-600 my-2">{ bike.type }</div>
                <h3 className="text-xl font-bold">{ bike.brand } { bike.model }</h3>
            </div>

            <div className="mb-5">
            { description }
            </div>

            <button onClick={ () => setShowFullDescription((prevState) => !prevState) } className="text indigo-500 mb-5 hover:text-indigo-600">
            { showFullDescription ? 'Less' : 'More' }
            </button>

            <h3 className="text-indigo-500 mb-2">Rent cost: { bike.rentCostPerDay } zł</h3>

            <div className="border border-gray-100 mb-5"></div>

            <div className="flex flex-col lg:flex-row justify-between mb-4">
                <Link
                to={`/bikes/${bike.id}`}
                className="h-[36px] bg-indigo-500 hover:bg-indigo-600 text-white px-4 py-2 rounded-lg text-center text-sm"
                >
                Details
                </Link>
            </div>
            </div>
        </div>
    );
};
export default BikeListing;