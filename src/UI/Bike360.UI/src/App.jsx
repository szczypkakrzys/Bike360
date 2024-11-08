import { 
  Route, 
  createBrowserRouter, 
  createRoutesFromElements, 
  RouterProvider 
} from 'react-router-dom';
import HomePage from './pages/HomePage';
import MainLayout from './layouts/MainLayout';
import BikesPage from './pages/BikesPage';
import NotFoundPage from './pages/NotFoundPage';
import BikeDetailsPage, {bikeLoader} from './pages/BikeDetailsPage';
import AddBikePage from './pages/AddBikePage';
import EditBikePage from './pages/EditBikePage';

const App = () => {
  
  const addBike = async (newBike) => {
    const res = await fetch('/api/bikes', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(newBike)
    });

    return;
  };

  const deleteBike = async (id) => {
    const res = await fetch(`/api/bikes/${id}`, {
      method: 'DELETE'
    });

    return;
  }

  const updateBike = async (bike) => {
    const res = await fetch(`/api/bikes/${bike.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(bike)
    });
    
    return;
  };

  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path='/' element={ <MainLayout /> }>
        <Route index element={ <HomePage /> }/>
        <Route path='/bikes' element={ <BikesPage /> }/>
        <Route path='/bikes/:id' element={ <BikeDetailsPage deleteBike={deleteBike}/> } loader={bikeLoader} />
        <Route path='/new-bike' element={ <AddBikePage  addBikeSubmit={addBike}/> }/>
        <Route path='*' element={ <NotFoundPage /> }/>
        <Route path='/edit-bike/:id' element={ <EditBikePage updateBikeSubmit={updateBike}/> } loader={bikeLoader} />

      </Route>
    )
  );
  
  return  <RouterProvider router={ router }/>;
};

export default App;