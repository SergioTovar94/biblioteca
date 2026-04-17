import { Outlet } from 'react-router-dom';
import Sidebar from './Sidebar';

function Layout(){
    return(
        <div className="min-h-screen flex">
            <Sidebar />
            <main className="flex-1 p-4">
                <Outlet />{}
            </main>
        </div>
    );
}

export default Layout;