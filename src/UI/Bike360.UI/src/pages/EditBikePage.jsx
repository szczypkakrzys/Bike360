import { useState } from "react";
import { useParams, useLoaderData, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const EditBikePage = ({updateBikeSubmit}) => {
    const bike = useLoaderData();

    const[brand, setBrand] = useState(bike.brand);
    const[type, setType] = useState(bike.type);
    const[model, setModel] = useState(bike.model);
    const[size, setSize] = useState(bike.size);
    const[color, setColor] = useState(bike.color);
    const[rentCostPerDay, setRentCostPerDay] = useState(bike.rentCostPerDay);
    const[frameNumber, setFrameNumber] = useState(bike.frameNumber);
    const[description, setDescription] = useState(bike.description);

    const navigate = useNavigate();
    const {id} = useParams();

    const submitForm = (e) => {
        e.preventDefault();

        const updatedBike = {
            id,
            brand,
            type,
            model,
            size,
            color,
            rentCostPerDay,
            frameNumber,
            description
        }
  
        updateBikeSubmit(updatedBike);
  
        toast.success('Bike udpated successfully')
  
        return navigate(`/bikes/${id}`);
    };

    return (
        <section className="bg-indigo-50">
        <div className="container m-auto max-w-2xl py-24">
          <div
            className="bg-white px-6 py-8 mb-4 shadow-md rounded-md border m-4 md:m-0"
          >
            <form onSubmit={submitForm}>
              <h2 className="text-3xl text-center font-semibold mb-6">Update Bike</h2>
  
              <div className="mb-4">
                <label htmlFor="type" className="block text-gray-700 font-bold mb-2"
                  >Bike Type</label
                >
                <select
                  id="type"
                  name="type"
                  className="border rounded w-full py-2 px-3"
                  required
                  value={type}
                  onChange={(e) => setType(e.target.value)}
                >
                  <option value="Gravel">Gravel Bike</option>
                  <option value="Road">Road Bike</option>
                  <option value="MTB">MTB</option>
                  <option value="E-Bike">E-Bike</option>
                </select>
              </div>
  
              <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2"
                  >Brand</label
                >
                <input
                  type="text"
                  id="title"
                  name="title"
                  className="border rounded w-full py-2 px-3 mb-2"
                  placeholder="Bike brand..."
                  required
                  value={brand}
                  onChange={(e) => setBrand(e.target.value)}
                />
              </div>

              <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2"
                  >Model</label
                >
                <input
                  type="text"
                  id="title"
                  name="title"
                  className="border rounded w-full py-2 px-3 mb-2"
                  placeholder="Model name..."
                  required
                  value={model}
                  onChange={(e) => setModel(e.target.value)}
                />
              </div>

              <div className="mb-4">
                <label
                  htmlFor="description"
                  className="block text-gray-700 font-bold mb-2"
                  >Description</label
                >
                <textarea
                  id="description"
                  name="description"
                  className="border rounded w-full py-2 px-3"
                  rows="4"
                  placeholder="Add any bike details, etc"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                ></textarea>
              </div>
  
              <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2"
                  >Rent cost per day</label
                >
                <input
                  type="text"
                  id="title"
                  name="title"
                  className="border rounded w-full py-2 px-3 mb-2"
                  placeholder="Cost..."
                  required
                  value={rentCostPerDay}
                  onChange={(e) => setRentCostPerDay(e.target.value)}
                />
              </div>

              <div className="mb-4">
                <label htmlFor="type" className="block text-gray-700 font-bold mb-2"
                  >Frame Size</label
                >
                <select
                  id="type"
                  name="type"
                  className="border rounded w-full py-2 px-3"
                  required
                  value={size}
                  onChange={(e) => setSize(e.target.value)}
                >
                  <option value="S">S</option>
                  <option value="M">M</option>
                  <option value="L">L</option>
                  <option value="XL">XL</option>
                </select>
              </div>

              <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2"
                  >Color</label
                >
                <input
                  type="text"
                  id="title"
                  name="title"
                  className="border rounded w-full py-2 px-3 mb-2"
                  placeholder="Bike color..."
                  required
                  value={color}
                  onChange={(e) => setColor(e.target.value)}
                />
              </div>
  
              <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2"
                  >Frame number</label
                >
                <input
                  type="text"
                  id="title"
                  name="title"
                  className="border rounded w-full py-2 px-3 mb-2"
                  placeholder="Frame number..."
                  required
                  value={frameNumber}
                  onChange={(e) => setFrameNumber(e.target.value)}
                />
              </div>

              <div>
                <button
                  className="bg-indigo-500 hover:bg-indigo-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                  type="submit"
                >
                  Submit
                </button>
              </div>
            </form>
          </div>
        </div>
      </section>  
    );
};
export default EditBikePage;