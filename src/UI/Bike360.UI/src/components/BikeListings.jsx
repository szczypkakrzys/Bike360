import { useState, useEffect } from 'react';
import BikeListing from './BikeListing';

const BikeListings = ({ isHomePage = false }) => {
  const [bikes, setBikes] = useState([]);
  const [loading, setLoading] = useState(true);
    
  useEffect(() => {
    const fetchBikes = async () => {
      try{
        const res = await fetch('/api/bikes');
        const data = await res.json();
        setBikes(data);
      } catch(error){
        console.log('Error fetching data', error);
      } finally {
        setLoading(false);
      }
    }

    fetchBikes();
  }, [])

  return (
<section className="bg-blue-50 px-4 py-10">
      <div className="container-xl lg:container m-auto">
        <h2 className="text-3xl font-bold text-indigo-500 mb-6 text-center">
          { isHomePage ? 'Recent Bikes' : 'Browse Bikes' }
        </h2>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            { loading ? (<h2>Loading...</h2>) :(
              <>
                { bikes.map((bike) => (
                  <BikeListing key={bike.id} bike={bike}/>
                  )) 
                } 
              </>
            )}
                  
        </div>
      </div>
    </section>
    );
};
export default BikeListings;