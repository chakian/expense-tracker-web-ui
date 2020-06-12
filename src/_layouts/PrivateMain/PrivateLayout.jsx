import React from 'react';
import { makeStyles, useTheme } from '@material-ui/styles';
// import { useMediaQuery } from '@material-ui/core';
import clsx from 'clsx';
import { Topbar, Sidebar, Footer } from './components';

const classes = makeStyles(theme => ({
    root: {
        paddingTop: 56,
        height: '100%',
        [theme.breakpoints.up('sm')]: {
            paddingTop: 64
        }
    },
    shiftContent: {
        paddingLeft: 240
    },
    content: {
        height: '100%'
    }
}));



const PrivateLayout = ({ children, props }) => {
    const theme = useTheme();
    // const isDesktop = useMediaQuery(theme.breakpoints.up('lg'), {
    //     defaultMatches: true
    // });
    const isDesktop = true;

    // const { authentication } = props;
    // const [openSidebar, setOpenSidebar] = useState(false);

    const handleSidebarOpen = () => {
        setOpenSidebar(true);
    };

    const handleSidebarClose = () => {
        setOpenSidebar(false);
    };

    // const shouldOpenSidebar = isDesktop ? true : openSidebar;
    const shouldOpenSidebar = true;

    return (
        <div
            className={clsx({
                [classes.root]: true,
                [classes.shiftContent]: isDesktop
            })}
        >
            <Topbar onSidebarOpen={handleSidebarOpen} />
            <Sidebar
                onClose={handleSidebarClose}
                open={shouldOpenSidebar}
                variant={isDesktop ? 'persistent' : 'temporary'}
            />
            <main className={classes.content}>
                {children}
                <Footer />
            </main>
            
        </div>
    );
}

export { PrivateLayout };