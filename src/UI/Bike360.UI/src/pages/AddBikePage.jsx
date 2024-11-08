import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const AddBikePage = ({ addBikeSubmit }) => {
    const[brand, setBrand] = useState('');
    const[type, setType] = useState('Gravel');
    const[model, setModel] = useState('');
    const[size, setSize] = useState('S');
    const[color, setColor] = useState('');
    const[rentCostPerDay, setRentCostPerDay] = useState('');
    const[frameNumber, setFrameNumber] = useState('');
    const[description, setDescription] = useState('');

    const navigate = useNavigate();

    const submitForm = (e) => {
      e.preventDefault();

      const newBike = {
        brand,
        type,
        model,
        size,
        color,
        rentCostPerDay,
        frameNumber,
        description
      }

      addBikeSubmit(newBike);

      toast.success('Bike added successfully')

      return navigate('/bikes');
    }

    return (
        <section className="bg-indigo-50">
        <div className="container m-auto max-w-2xl py-24">
          <div
            className="bg-white px-6 py-8 mb-4 shadow-md rounded-md border m-4 md:m-0"
          >
            <form onSubmit={submitForm}>
              <h2 className="text-3xl text-center font-semibold mb-6">New Bike</h2>
  
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
                  Add Bike
                </button>
              </div>
            </form>
          </div>
        </div>
      </section>  
    );
};
export default AddBikePage;