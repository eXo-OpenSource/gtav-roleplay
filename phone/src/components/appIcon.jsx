export const AppIcon = ({ id, nameLocale, name, Icon, backgroundColor, color, notification }) => {
    

    return (
        <div>
            <div className="text-5xl">
                <Icon fontSize="inherit"/>
            </div>
            <span>{nameLocale}</span>
        </div>
    );
};