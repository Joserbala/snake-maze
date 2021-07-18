const SUCCESS = true;
const FAILURE = false;
const VirtualCurrency = 'VirtualCurrency';
const HCCode = 'HC';
const SCCode = 'SC';

handlers.CreateAccount = () => {
    var HighScore = {
        Score: 0
    };

    var result = UpdateUserReadOnlyData({
        HighScore: JSON.stringify(HighScore)
    });

    // Error control in client.
    log.info(JSON.stringify(result));
};

handlers.UpdateScore = (args) => {
    var highScore = args.highScore;

    var HighScore = {
        Score: highScore
    };

    var result = UpdateUserReadOnlyData({
        HighScore: JSON.stringify(HighScore)
    });

    // Error control in client.
    log.info(JSON.stringify(result));
};

handlers.GetLoginData = () => {
    var readOnlyData = GetUserReadOnlyData().Data;
    var catalog = GetCatalogItems();
    var inventory = GetUserInventory();
    var currency = GetCurrency(inventory);

    var loginData = {
        readOnlyData: readOnlyData,
        catalog: catalog,
        inventory: inventory.Inventory,
        currency: currency
    };

    return { loginData: loginData };
};

handlers.GetCurrency = () => {
    var currency = {
        currency: GetCurrency(GetUserInventory())
    };

    return currency;
};

handlers.AddSCCurrency = (args) => {
    var amount = args.amount;

    try {
        var result = AddUserUserVirtualCurrency(amount, SCCode);
        log.info(result);

        return { isSuccess: SUCCESS, balance: result.Balance };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

/////////////////////////////////////////////////////////////////////////
//
//                          SERVER CALLS
//
/////////////////////////////////////////////////////////////////////////

/**
 * 
 * @param {object} data - The object with all the data we want to update.
 * @returns {object} .DataVersion has the version that has been set after the update.
 * @throws Will throw an error if the API encounters an error.
 */
function UpdateUserReadOnlyData(data) {
    var request = {
        PlayFabId: currentPlayerId,
        Data: data
    };

    return server.UpdateUserReadOnlyData(request);
};

/**
 * Gets the ReadOnlyData from the user with PlayFabId = currentPlayerId.
 * 
 * @param {undefined} data - Do not set any value in this parameter.
 * @returns {object} All the ReadOnlyData, access it with .Data
 * @throws Will throw an error if the API encounters an error.
 */
function GetUserReadOnlyData(data) {
    var request = {
        PlayFabId: currentPlayerId,
        Data: data
    };

    return server.GetUserReadOnlyData(request);
};

/**
 * 
 * @returns {object} The inventory of the user with currentPlayerId.
 * @throws Will throw an error if the API encounters an error.
 */
function GetUserInventory() {
    var request = {
        PlayFabId: currentPlayerId
    }

    return server.GetUserInventory(request);
};

/**
 * 
 * @param {string} [catalogVersion=null] - Which catalog is requested.
 * @returns {object[]} List of items belonging to catalogVersion.
 * @throws Will throw an error if the API encounters an error.
 */
function GetCatalogItems(catalogVersion = null) {
    var request = {
        CatalogVersion: catalogVersion
    };

    return server.GetCatalogItems(request);
};

/**
 * Adds a certain amount of currency to virtualCurrency.
 * 
 * @param {number} amount - The amount to be added to virtualCurrency
 * @param {string} virtualCurrency - The code for the virtual currency to change.
 * @returns {oject} Access with .Balance to check the balance of the user.
 * @throws Will throw an error if the API encounters an error.
 */
function AddUserUserVirtualCurrency(amount, virtualCurrency) {
    var request = {
        Amount: amount,
        PlayFabId: currentPlayerId,
        VirtualCurrency: virtualCurrency
    };

    return server.AddUserVirtualCurrency(request);
};

/////////////////////////////////////////////////////////////////////////
//
//                               UTILS
//
/////////////////////////////////////////////////////////////////////////

function GetCurrency(inventory) {
    var currency = {
        softCoins: inventory[VirtualCurrency][SCCode],
        hardCoins: inventory[VirtualCurrency][HCCode]
    };

    return currency;
};