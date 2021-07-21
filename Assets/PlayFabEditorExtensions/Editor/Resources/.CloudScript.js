const SUCCESS = true;
const FAILURE = false;
const HC_CODE = 'HC';
const SC_CODE = 'SC';
const VirtualCurrency = 'VirtualCurrency';

handlers.CreateAccount = function () {
    var HighScore = {
        Score: 0
    };
    var Skins = {
        Snake: 'Default',
        Maze: 'Default'
    };

    try {
        var result = UpdateUserReadOnlyData({
            HighScore: JSON.stringify(HighScore),
            Skins: JSON.stringify(Skins)
        });
        log.info(result);

        return { isSuccess: SUCCESS, error: null };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

handlers.UpdateScore = function (args) {
    var highScore = args.highScore;

    var HighScore = {
        Score: highScore
    };

    try {
        var result = UpdateUserReadOnlyData({
            HighScore: JSON.stringify(HighScore)
        });
        log.info(result);

        return { isSuccess: SUCCESS, error: null };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

handlers.UpdateCurrentSkins = function (args) {
    var snakeSkin = args.snakeSkin;
    var mazeSkin = args.mazeSkin;

    var Skins = {
        Snake: snakeSkin,
        Maze: mazeSkin
    };

    try {
        var result = UpdateUserReadOnlyData({
            Skins: JSON.stringify(Skins)
        });
        log.info(result);

        return { isSuccess: SUCCESS, error: null };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

handlers.GetLoginData = function () {
    try {
        var readOnlyData = GetUserReadOnlyData().Data;

        try {
            var catalog = GetCatalogItems().Catalog;

            try {
                var inventory = GetUserInventory();
                var currency = GetCurrency(inventory);

                var loginData = {
                    readOnlyData: readOnlyData,
                    catalog: catalog,
                    inventory: inventory.Inventory,
                    currency: currency
                };

                log.info(loginData);

                return { isSuccess: SUCCESS, loginData: loginData };

            } catch (error) {
                log.error("[ERROR GETTING THE USER INVENTORY] " + error);

                return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
            }
        } catch (error) {
            log.error("[ERROR GETTING THE CATALOG] " + error);

            return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
        }
    } catch (error) {
        log.error("[ERROR GETTING THE USER READ ONLY DATA] " + error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

handlers.GetCurrency = function () {
    try {
        var currency = GetCurrency(GetUserInventory())
        log.info(currency);

        return { isSuccess: SUCCESS, currency: currency };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

/**
 * 
 * @param {object} args - The arguments from ExecuteCloudScript.
 * @param {string} args.itemId - The id of the item to search for.
 * @returns If there is no error in the API and exists an item with ItemId = itemId, returns the ItemInstance with itemId.
 */
handlers.GetItemFromInventory = function (args) {
    try {
        var inventory = GetUserInventory().Inventory;
        log.info(inventory);

        var itemId = args.itemId;
        var itemInstance;

        for (var index = 0; index < inventory.length; index++) {
            if (inventory[index].ItemId == itemId) {
                itemInstance = inventory[index];
                break;
            }
        }

        if (itemInstance == null) {
            log.error("No item found.");

            return { isSuccess: FAILURE, error: "No item found." };
        }

        log.info(itemInstance);

        return { isSuccess: SUCCESS, itemInstance: itemInstance };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

handlers.AddSCCurrency = function (args) {
    var amount = args.amount;

    try {
        var result = AddUserUserVirtualCurrency(amount, SC_CODE);
        log.info(result);

        return { isSuccess: SUCCESS, balance: result.Balance };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

/**
 *
 * @param {object} args - The arguments from ExecuteCloudScriptRequest.
 * @param {string} args.itemInstanceId - The unique identifier of the puchased item.
 * @param {string} args.itemId - The item's id from the catalog.
 * @returns Wether the function was executed succesfully or not; if fails, in error there will be a vague description of the error.
 */
handlers.UpdateUserInventoryItemCustomData = function (args) {
    var itemInstanceId = args.itemInstanceId;
    var itemId = args.itemId;

    try {
        var catalog = GetCatalogItems().Catalog;
        var itemCustomData;

        for (let index = 0; index < catalog.length; index++) {
            if (catalog[index].ItemId == itemId) {
                itemCustomData = JSON.parse(catalog[index].CustomData);
                break;
            }
        }

        try {
            var result = UpdateUserInventoryItemCustomData(itemInstanceId, itemCustomData);
            log.info(result);

            return { isSuccess: SUCCESS };
        } catch (error) {
            log.error("[ERROR UPDATING THE USER INVENTORY] " + error);

            return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
        }

    } catch (error) {
        log.error(["ERROR GETTING THE CATALOG ITEMS"] + error);

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
 * @returns {object} The inventory of the user with currentPlayerId. To access the inventory, use .Inventory.
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
 * @returns {object[]} Access with .Catalog to obtain the List of items belonging to catalogVersion.
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
 * @returns {object} Access with .Balance to check the balance of the user.
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

/**
 *
 * @param {string} itemInstanceId - Unique identifier of the item to add to the user inventory.
 * @param {object} data - Dictionary to be written to the custom data.
 * @throws Will throw an error if the API encounters an error.
 */
function UpdateUserInventoryItemCustomData(itemInstanceId, data) {
    var request = {
        ItemInstanceId: itemInstanceId,
        PlayFabId: currentPlayerId,
        Data: data
    };

    server.UpdateUserInventoryItemCustomData(request);
};

/////////////////////////////////////////////////////////////////////////
//
//                               UTILS
//
/////////////////////////////////////////////////////////////////////////

function GetCurrency(inventory) {
    var currency = {
        softCoins: inventory[VirtualCurrency][SC_CODE],
        hardCoins: inventory[VirtualCurrency][HC_CODE]
    };

    return currency;
};