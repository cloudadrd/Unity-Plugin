using System;

public interface AdtInitListener {
    void onSuccess();

    void onError(string message);
}
