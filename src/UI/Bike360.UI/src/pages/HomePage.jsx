import Hero from "../components/Hero";
import HomeCards from "../components/HomeCards";
import BikeListings from "../components/BikeListings";
import ViewAllBikes from "../components/ViewAllBikes";

const HomePage = () => {
    return (
    <>
        <Hero title='Bike rental main page' subtitle='Subtitle'/>
        <HomeCards />
        <BikeListings isHomePage={true}/>
        <ViewAllBikes />
    </>
    );
};
export default HomePage;